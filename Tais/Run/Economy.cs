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

        [JsonProperty]
        int oweMonths { get; set; }

        decimal incomeTotal { get; }
        decimal outputTotal { get; }

        decimal surplus { get; }

        List<IncomeDetail> incomes { get; }

        List<OutputDetail> outputs { get; }

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

        public int oweMonths { get; set; }
        public decimal incomeTotal => incomes.Sum(x => x.value);
        public decimal outputTotal => outputs.Sum(x => x.value);

        public decimal surplus => incomeTotal - outputTotal;

        public List<IncomeDetail> incomes { get { return _incomes; } set { _incomes = value; } }
        public List<OutputDetail> outputs { get { return _outputs; } set { _outputs = value; } }

        public List<IncomeDetail> _incomes = new List<IncomeDetail>();
        public List<OutputDetail> _outputs = new List<OutputDetail>();

        public void DaysInc((int y, int m, int d) date)
        {
            if(date.d == 30)
            {

                currValue += surplus;

                oweMonths = currValue < 0 ? oweMonths + 1 : 0;
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(surplus)));
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(surplus)));
        }
    }
}