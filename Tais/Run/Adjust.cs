using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    class Adjust : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name;

        public decimal[] rates;

        public int currLevel { get; set; }

        public decimal currRate => rates[currLevel];

        public Adjust(IAdjust def)
        {
            name = def.GetType().BaseType.FullName;
            rates = def.level_rates;
            currLevel = def.init_level;
        }

        public Adjust()
        {

        }
    }
}
