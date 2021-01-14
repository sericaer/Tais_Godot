using System;
using Tais.API;

namespace Tais.Mod.Condition
{
    internal class Less<T> : ConditionDef where T : unmanaged, IComparable, IEquatable<T>
    {
        private IVisitorR<T> left;
        private T right;

        public Less(IVisitorR<T> left, T right)
        {
            this.left = left;
            this.right = right;
        }

        public bool isTrue()
        {
            return left.GetValue().CompareTo(right) < 0;
        }
    }
}