using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        public IEnumerable<IPop> pops => departs.SelectMany(d => d.pops);

        internal List<Integration> integrations = new List<Integration>();

        public static Runner Deserialize(string content)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            var obj = JsonConvert.DeserializeObject<Runner>(content, settings);
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

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            LOG.INFO("1");

            this.SetIntegration(adjusts[ADJUST_TYPE.POP_TAX], pops).With(x => x.percent, y => y.UpdateTaxPercent);

            LOG.INFO("2");
            this.SetIntegration(adjusts[ADJUST_TYPE.CHAOTING_TAX], chaoting).With(x => x.percent, y => y.UpdateReportTaxPercent);

            LOG.INFO("3");
            this.SetIntegration(departs, economy).With(x => x.incomeDetail, y => y.UpdateIncome);

            LOG.INFO("4");
            this.SetIntegration(date, taishou).With(x=>x.value, y=>y.DaysInc, true);

            LOG.INFO("5");
            this.SetIntegration(date, economy).With(x=>x.value, y=>y.DaysInc, true);

            this.SetIntegration(date, chaoting).With(x => x.value, y => y.DaysInc, true);

            this.SetIntegration(date, eventMgr).With(x => x.value, y => y.DaysIncAsync, true);

            LOG.INFO("6");
            this.SetIntegration(chaoting, economy).With(x => x.outputDetail, y => y.UpdateOutput);

            LOG.INFO(chaoting.reportTax);
            LOG.INFO(chaoting.expectTax);
            LOG.INFO(chaoting.currTaxLevel);

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
