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
    class Depart : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public readonly string name;
        public readonly Color color;

        public Pop[] pops;

        public int popNum => popNumDetail.Sum(x => x.value);

        public IEnumerable<(string name, int value)> popNumDetail { get; set; }


        public Depart(IDepart def)
        {
            name = def.GetType().FullName;
            pops = def.pops.Select(x => new Pop(x)).ToArray();
            color = def.color;

            IntegrateData();
        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            pops.Where(pop => pop.isTax).ToOBSPropertyList(pop => pop.num).Subscribe(change=>
            {
                popNumDetail = change.Select(elem => (elem.Sender.name, (int)elem.Value));
                //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("popNumDetail"));
            });
        }
    }

    public class Detail<T>
    {
        public string name;
        public T value;

        public Detail(string name, T value)
        {
            this.name = name;
            this.value = value;
        }
    }

    public class TEST
    {
        public decimal a;
    }
}
