using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
        public Taishou taishou;

        [JsonProperty]
        public Date date;

        private List<Integration> list;

        public static Runner Deserialize(string content)
        {
            var obj = JsonConvert.DeserializeObject<Runner>(content);
            return obj;
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public Runner(Init.Initer initer)
        {
            taishou = new Taishou(initer.name, initer.age, initer.party);

            date = new Date();

            DataReactive(new StreamingContext());
        }

        [OnDeserialized]
        private void DataReactive(StreamingContext context)
        {
            //date.PropertyIntegration(x => x.total_days, taishou.DaysInc);
        }

        private void Integration<TObj, TReturn>(TObj date, Expression<Func<TObj, TReturn>> propertyExpression, Action<TReturn> onDaysInc)
        {
            throw new NotImplementedException();
        }
    }

    class Integration
    {
        public IDisposable disposable;
        //public 
    }
}
