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

        decimal incomeTotal { get; }

        List<IncomeDetail> incomes { get; }
        void DaysInc((int y, int m, int d) date);

        void UpdateIncome(IncomeDetail detail);

        void UpdateOutput(OutputDetail detail);
    }

    class Economy : IEconomy
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public decimal currValue { get; set; }

        public decimal incomeTotal => incomes.Sum(x => x.value);
        public decimal outputTotal => outputs.Sum(x => x.value);

        public List<IncomeDetail> incomes { get { return _incomes; } set { _incomes = value; } }
        public List<OutputDetail> outputs { get { return _outputs; } set { _outputs = value; } }

        public List<IncomeDetail> _incomes = new List<IncomeDetail>();
        public List<OutputDetail> _outputs = new List<OutputDetail>();

        public void DaysInc((int y, int m, int d) date)
        {
            if(date.d == 30)
            {
                currValue += incomeTotal;
            }
        }

        public void UpdateIncome(IncomeDetail detail)
        {
            var find = incomes.Find(x => x.name == detail.name);
            if (find != null)
            {
                incomes.Remove(find);
            }

            incomes.Add(detail);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(incomes)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(incomeTotal)));
        }

        public void UpdateOutput(OutputDetail detail)
        {
            var find = outputs.Find(x => x.name == detail.name);
            if (find != null)
            {
                outputs.Remove(find);
            }

            outputs.Add(detail);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(outputs)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(outputTotal)));
        }
    }
}