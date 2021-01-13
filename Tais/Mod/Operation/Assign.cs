using Tais.API;

namespace Tais.Mod.Operation
{
    public class Assign<T> : IOperation
    {
        private IVisitor<T> target;
        private T value;

        public Assign(IVisitor<T> target, T value)
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
