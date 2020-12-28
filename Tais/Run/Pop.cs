using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    class Pop : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        [JsonProperty]
        public readonly string name;

        [JsonProperty]
        public readonly bool isTax;

        [JsonProperty]
        public decimal num { get; set; }

        public Pop(IPop def)
        {
            name = def.GetType().FullName;
            num = def.num;
            isTax = def.is_tax;
        }

        [JsonConstructor]
        private Pop()
        {

        }
    }
}
