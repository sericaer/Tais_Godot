using System;
using System.Linq.Expressions;
using Tais.API;

namespace Tais.Mod.Condition
{
    internal class Greater<T> : ConditionBase<T> where T:IComparable
    {
        public Greater(IVisitorR<T> left, T right) : base(left, right)
        {
        }

        public Greater(IVisitorR<T> left, IVisitorR<T> right, Expression<Func<T, T>> lamda) : base(left, right, lamda)
        {
        }

        public override bool Result(T left, T right)
        {
            return left.CompareTo(right) > 0;
        }
    }
}