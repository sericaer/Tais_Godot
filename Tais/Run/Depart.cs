using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    class Depart : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        [JsonProperty]
        public readonly string name;

        [JsonProperty]
        public readonly Color color;

        [JsonProperty]
        public Pop[] pops;

        public int popNum => popNumDetail.Sum(x => x.value);

        public IEnumerable<(string name, int value)> popNumDetail { get; set; }


        public decimal tax => taxDetail.Sum(x => x.value);
        public IEnumerable<(string name, decimal value)> taxDetail { get; set; }

        public Depart(IDepart def)
        {
            name = def.GetType().FullName;
            pops = def.pops.Select(x => new Pop(x)).ToArray();
            color = def.color;

            IntegrateData();
        }

        [OnDeserialized]
        private void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            pops.Where(pop => pop.isTax).ToOBSPropertyList(pop => pop.num).Subscribe(change=>
            {
                popNumDetail = change.Select(elem => (elem.Sender.name, (int)elem.Value));
            });

            pops.Where(pop => pop.isTax).ToOBSPropertyList(pop => pop.tax).Subscribe(change =>
            {
                taxDetail = change.Select(elem => (elem.Sender.name, elem.Value));
            });
        }

        [JsonConstructor]
        private Depart()
        {

        }
    }
}
