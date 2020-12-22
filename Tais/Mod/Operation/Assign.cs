using Tais.API;

namespace Tais.Mod.Operation
{
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
