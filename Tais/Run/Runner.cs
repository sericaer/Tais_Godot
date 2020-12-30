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

        public IEconomy economy;

        [JsonProperty]
        public List<Depart> departs = new List<Depart>();

        [JsonProperty]
        public List<Adjust> adjusts = new List<Adjust>();

        public List<Integration> integrations = new List<Integration>();

        public IEnumerable<IPop> pops => departs.SelectMany(d => d.pops);

        public bool isInitialized => integrations.Any();

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

            runner.date = new Date();
            runner.economy = new Economy();

            runner.taishou = new Taishou(initer.name, initer.age, initer.party);
            runner.departs.AddRange(modder.departs.Select(x => Depart.Gen(x)));
            runner.adjusts.AddRange(modder.adjusts.Select(x => new Adjust(x)));
            
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
            var taxAdjust = adjusts.Single(x => x.name == typeof(AdjustTaxDef).FullName);
            this.SetIntegration(taxAdjust, pops).With(x => x.currRate, y => y.UpdateTaxRate);

            this.SetIntegration(departs, economy).With(x => x.incomeDetail, y => y.UpdateIncome);

            this.SetIntegration(date, taishou).With(x=>x.value, y=>y.DaysInc);
            this.SetIntegration(date, economy).With(x=>x.value, y=>y.DaysInc);

        }
    }
}
