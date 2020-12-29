using System.Collections.Generic;
using System.Linq;

namespace Tais.Run
{
    interface IEconomy
    {
        void DaysInc(decimal total_days);

        void UpdateIncome(IncomeDetail detail);
    }

    class Economy : IEconomy
    {
        public decimal currValue { get; set; }

        public List<IncomeDetail> incomes = new List<IncomeDetail>();

        public void DaysInc(decimal total_days)
        {
            currValue += incomes.Sum(x => x.value);
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