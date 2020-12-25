using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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

        public List<Integration> integrations = new List<Integration>();

        internal bool isInitialized => integrations.Any();


        public static Runner Deserialize(string content)
        {
            var obj = JsonConvert.DeserializeObject<Runner>(content);
            return obj;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        //public Runner(Init.Initer initer)
        //{
        //    taishou = new Taishou(initer.name, initer.age, initer.party);

        //    date = new Date();

        //    DataReactive(new StreamingContext());
        //}

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            SetIntegration(date, taishou).With(x=>x.total_days, y=>y.DaysInc);
        }

        private IntegrationImp<TS, TD> SetIntegration<TS, TD>(TS source, TD dest) where TS : class, INotifyPropertyChanged
        {
            IntegrationImp<TS, TD> integration = integrations.SingleOrDefault(x => x.GetType().GetGenericArguments()[0] == typeof(TS)
                                               && x.GetType().GetGenericArguments()[1] == typeof(TD)) as IntegrationImp<TS, TD>;
            if(integration == null)
            {
                integration = new IntegrationImp<TS, TD>(source, dest);
                integrations.Add(integration);
            }

            return integration;
        }
    }
}
