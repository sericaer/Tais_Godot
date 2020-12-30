using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IEconomy:INotifyPropertyChanged
    {
        [JsonProperty]
        decimal currValue { get; set; }

        void DaysInc((int y, int m, int d) date);

        void UpdateIncome(IncomeDetail detail);
    }

    class Economy : IEconomy
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public decimal currValue { get; set; }

        public List<IncomeDetail> incomes = new List<IncomeDetail>();

        public void DaysInc((int y, int m, int d) date)
        {
            if(date.d == 30)
            {
                currValue += incomes.Sum(x => x.value);
            }
        }

        public void UpdateIncome(IncomeDetail detail)
        {
            var find = incomes.Find(x => x.name == detail.name);
            if(find != null)
            {
                incomes.Remove(find);
            }

            incomes.Add(detail);
        }
    }
}