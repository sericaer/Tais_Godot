using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais
{
    public interface IPop
    {
    }

    public interface IEvent
    {
        IOperation op { get; }
    }

    public interface IOperation
    {
        void Do();
    }

    public class Assign : IOperation
    {
        private IVisitor target;
        private decimal value;

        public Assign(IVisitor target, decimal value)
        {
            this.target = target;
            this.value = value;
        }

        public void Do()
        {
            target.SetValue(value);
        }
    }
}
