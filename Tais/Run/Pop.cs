using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    class Pop : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public readonly string name;
        public readonly bool isTax;

        public decimal num { get; set; }

        public Pop(IPop def)
        {
            name = def.GetType().FullName;
            num = def.num;
            isTax = def.is_tax;
        }


    }
}
