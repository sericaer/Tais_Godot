using System.Collections.Generic;

namespace Tais.Run
{
    interface IEconomy
    {
        void DaysInc(decimal total_days);

        void UpdateIncome(IncomeDetail detail);
    }

    class Economy : IEconomy
    {
        public List<IncomeDetail> incomes = new List<IncomeDetail>();

        public void DaysInc(decimal total_days)
        {

        }

        public void UpdateIncome(IncomeDetail detail)
        {
            //incomes = 0l
        }
    }
}