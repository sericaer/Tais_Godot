using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    class Depart : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        [JsonProperty]
        public string name;

        [JsonProperty]
        public Color color;

        [JsonProperty]
        public IPop[] pops;

        public int popNum => popNumDetail.Sum(x => x.value);

        public IEnumerable<(string name, int value)> popNumDetail { get; set; }

        public decimal tax => incomeDetail.value;

        public IncomeDetail incomeDetail => _taxDetail;

        private IncomeDetail _taxDetail;

        public static Depart Gen(IDepartDef def)
        {
            var depart = new Depart();

            depart.name = def.GetType().FullName;
            depart.color = def.color;
            depart.pops = def.pops.Select(x => Pop.Gen(x)).ToArray();

            depart.IntegrateData();

            return depart;
        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            _taxDetail = new IncomeDetail(name);

            pops.ToOBSPropertyList(pop => pop.num).Subscribe(change=>
            {
                popNumDetail = change.Select(elem => (elem.Sender.name, (int)elem.Value));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("popNumDetail"));
            });

            pops.ToOBSPropertyList(pop => pop.tax).Subscribe(change =>
            {
                incomeDetail.Update(IncomeDetail.TYPE.POP_TAX, change.Select(elem => new Detail_Leaf(elem.Sender.name, elem.Value)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("incomeDetail"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("tax"));
            });
        }
    }
}
