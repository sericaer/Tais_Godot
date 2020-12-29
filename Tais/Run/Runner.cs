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

        public IEnumerable<Pop> pops => departs.SelectMany(d => d.pops);

        public bool isInitialized => integrations.Any();

        public static Runner Deserialize(string content)
        {
            var settings = new JsonSerializerSettings();
            settings.TypeNameHandling = TypeNameHandling.Objects;
            var obj = JsonConvert.DeserializeObject<Runner>(content, settings);
            return obj;
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
            this.SetIntegration(date, taishou).With(x=>x.total_days, y=>y.DaysInc);
            this.SetIntegration(date, economy).With(x=>x.total_days, y=>y.DaysInc);

            this.SetIntegration(departs, economy).With(x=>x.incomeDetail, y=>y.UpdateIncome);

            var taxAdjust = adjusts.Single(x => x.name == typeof(IAdjustTaxDef).FullName);
            this.SetIntegration(taxAdjust, pops).With(x => x.currRate, y =>y.UpdateTaxRate);
        }
    }
}
