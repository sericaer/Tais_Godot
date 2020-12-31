﻿using Newtonsoft.Json;
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

        [DependsOn("num", "tax_rate")]
        public IBuffedValue tax { get; set; }

        private decimal tax_rate { get; set; }

        public static Pop Gen(PopDef def)
        {
            var pop = new Pop();

            pop.name = def.GetType().FullName;
            pop.num = def.num;
            pop.isTax = def.is_tax;
            pop.tax = new BuffedValue();

            pop.IntegrateData();

            return pop;
        }

        public void UpdateTaxRate(decimal rate)
        {
            tax_rate = rate;
        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            this.OBSProperty(x => x.num).Subscribe(_ => UpdateTax());
            this.OBSProperty(x => x.tax_rate).Subscribe(_ => UpdateTax());
        }

        private void UpdateTax()
        {
            tax.baseValue = num * tax_rate;
        }
    }
}
