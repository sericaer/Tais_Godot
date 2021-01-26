using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Tais;
using Tais.API;
using Tais.Init;
using Tais.Mod;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    class Runner
    {
        public bool gmEnd
        {
            get
            {
                return false;
            }
            set
            {
                if(value)
                {
                    eventMgr.currEvent = new EndEvent();
                }
            }
        }

        [JsonProperty]
        public ITaishou taishou;

        [JsonProperty]
        public IDate date;

        [JsonProperty]
        public IEconomy economy;

        [JsonProperty]
        public IChaoting chaoting;

        [JsonProperty]
        public List<Depart> departs = new List<Depart>();

        [JsonProperty]
        public Dictionary<ADJUST_TYPE, IAdjust> adjusts = new Dictionary<ADJUST_TYPE, IAdjust>();

        public IEventManager eventMgr;

        public IAdjust adjustReportChaotingTax => adjusts[ADJUST_TYPE.CHAOTING_TAX];

        public IEnumerable<IPop> pops => departs.SelectMany(d => d.pops);

        internal List<Integration> integrations = new List<Integration>();

        public static Runner Deserialize(string content, Modder modder)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            var obj = JsonConvert.DeserializeObject<Runner>(content, settings);

            obj.eventMgr = EventManager.Gen(modder.events);
            obj.IntegrateData();

            return obj;
        }

        public static Runner Gen(Initer initer, Modder modder)
        {
            var runner = new Runner();

            foreach (ADJUST_TYPE type in Enum.GetValues(typeof(ADJUST_TYPE)))
            {
                var def = modder.adjusts.Single(x => x.type == type);
                runner.adjusts.Add(type, new Adjust(def));
            }

            runner.date = new Date();
            runner.economy = new Economy();

            runner.taishou = new Taishou(initer.name, initer.age, initer.party);
            runner.departs.AddRange(modder.departs.Select(x => Depart.Gen(x)));

            runner.chaoting = Chaoting.Gen(modder.chaoting, initer.chaoting_tax_level, runner.pops.Where(x=>x.isTax).Sum(x=>(int)x.num));

            runner.eventMgr = EventManager.Gen(modder.events);

            runner.IntegrateData();

            return runner;
        }

        public string Serialize()
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            return JsonConvert.SerializeObject(this, Formatting.Indented, settings);
        }

        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            this.SetIntegration(adjusts[ADJUST_TYPE.POP_TAX], pops).With(x => x.percent, y => y.UpdateTaxPercent);
            this.SetIntegration(adjusts[ADJUST_TYPE.CHAOTING_TAX], chaoting).With(x => x.percent, y => y.UpdateReportTaxPercent);

            this.SetIntegration(departs, economy).With(x => x.incomeDetail, y => y.UpdateIncome);

            this.SetIntegration(date, taishou).With(x=>x.value, y=>y.DaysInc, true);
            this.SetIntegration(date, economy).With(x=>x.value, y=>y.DaysInc, true);
            this.SetIntegration(date, chaoting).With(x => x.value, y => y.DaysInc, true);
            this.SetIntegration(date, eventMgr).With(x => x.value, y => y.DaysIncAsync, true);

            this.SetIntegration(chaoting, economy).With(x => x.outputDetail, y => y.UpdateOutput);

            chaoting.OBSProperty(x => x.powerStatus).Select(x => x.effect.min_chaoting_report_tax_level)
                .Subscribe(x=> adjustReportChaotingTax.UpdateMinLevel(x));
        }
    }

    public interface IEffect : INotifyPropertyChanged
    {
        string type { get; }

        string name { get; }
        decimal value { get; }
        
        bool enable { get; }
    }
        
}
