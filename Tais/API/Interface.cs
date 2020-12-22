using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
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

    public class Assign<T> : IOperation
    {
        private IVisitor target;
        private T value;

        public Assign(IVisitor target, T value)
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
