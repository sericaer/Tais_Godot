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
    class Adjust : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        [JsonProperty]
        public string name;

        internal bool IsDefType<T>()
        {
            return name == typeof(T).FullName;
        }

        [JsonProperty]
        public decimal[] rates;

        [JsonProperty]
        public int currLevel { get; set; }

        public decimal currRate => rates[currLevel];

        public Adjust(AdjustDef def)
        {
            name = def.GetType().BaseType.FullName;

            rates = def.level_rates;
            currLevel = def.init_level;
        }

        
        [JsonConstructor]
        public Adjust()
        {

        }
    }
}
