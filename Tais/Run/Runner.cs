using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    class Runner
    {
        public decimal a { get; set; }

        [JsonProperty]
        public Taishou taishou;

        [JsonProperty]
        public Date date;

        public Runner(Init.Initer initer)
        {
            taishou = new Taishou(initer.name, initer.age, initer.party);

            date = new Date();

            date.OBSProperty(x => x.total_days).Subscribe(_ => onDaysInc());
        }

        private void onDaysInc()
        {

        }

        public void Test()
        {
            var DLL = Assembly.LoadFile(@"C:\Users\fang\source\repos\Tais\TaisGodot\Release\Tais\mod\Native\package\assembly.dll");

            var types = DLL.GetTypes();
            LOG.INFO(types.Select(x => x.Name as object).ToArray());

            var type = types.First(x => x.GetInterfaces().Contains(typeof(IEvent)));


            var eventobj = Activator.CreateInstance(type) as IEvent;

            eventobj.op.Do();
        }

        internal string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
