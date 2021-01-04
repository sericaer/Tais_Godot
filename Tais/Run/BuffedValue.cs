using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.Run
{
    interface IBuffedValue : INotifyPropertyChanged
    {
        decimal value { get; }
        decimal baseValue { get; set; }

        List<(string name, decimal value)> buffers { get; set; }
    }

    class BuffedValue : IBuffedValue
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public decimal value => baseValue + buffers.Sum(x=>x.value);

        public decimal baseValue { get; set; }
        public List<(string name, decimal value)> buffers { get { return _buffers; } set { _buffers = value; } }

        private List<(string name, decimal value)> _buffers = new List<(string name, decimal value)>();

        public void UpdateBuffer(string name, decimal value)
        {

        }
    }
}
