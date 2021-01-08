using Newtonsoft.Json;
using PropertyChanged;
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
    interface IPop : INotifyPropertyChanged
    {
        [JsonProperty]
        string name { get; }

        [JsonProperty]
        bool isTax { get; }

        [JsonProperty]
        decimal num { get; set; }

        IBuffedValue tax { get; }

        void UpdateTaxPercent(int rate);
    }

    class Pop : IPop
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public string name { get; set; }

        public bool isTax { get; set; }

        public decimal num { get; set; }

        public IBuffedValue tax { get; set; }

        private int taxPercent { get; set; }

        public decimal tax_rate => 0.01M;

        public void UpdateTaxPercent(int percent)
        {
            taxPercent = percent;
            UpdateTaxBase();
        }

        public static Pop Gen(PopDef def)
        {
            var pop = new Pop();

            pop.name = def.GetType().FullName;
            pop.num = def.num;
            pop.isTax = def.is_tax;

            pop.IntegrateData();

            return pop;
        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            tax = new BuffedValue();

            this.OBSProperty(x => x.num).Subscribe(_ => UpdateTaxBase());
        }

        private void UpdateTaxBase()
        {
            if(isTax)
            {
                tax.baseValue = num * tax_rate * taxPercent / 100M;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(tax)));
            }
        }
    }
}
