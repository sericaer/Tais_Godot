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
    public interface IPop : INotifyPropertyChanged
    {
        [JsonProperty]
        string name { get; }

        [JsonProperty]
        bool isTax { get; }

        [JsonProperty]
        decimal num { get; set; }

        decimal tax { get; }

        void UpdateTaxRate(decimal rate);
    }

    class Pop : IPop
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get; set; }

        public bool isTax { get; set; }

        public decimal num { get; set; }

        public decimal tax => num * tax_rate;

        private decimal tax_rate { get; set; }

        public static Pop Gen(PopDef def)
        {
            var pop = new Pop();

            pop.name = def.GetType().FullName;
            pop.num = def.num;
            pop.isTax = def.is_tax;

            return pop;
        }

        public void UpdateTaxRate(decimal rate)
        {
            tax_rate = rate;
        }
    }
}
