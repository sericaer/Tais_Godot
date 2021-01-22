using System;
using System.Linq.Expressions;
using Tais.API;

namespace Tais.Mod.Condition
{
    internal class Less : ConditionBase<decimal>
    {
        public Less(IVisitorR<decimal> left, decimal right) : base(left, right)
        {
        }

        public Less(IVisitorR<decimal> left, IVisitorR<decimal> right, Expression<Func<decimal, decimal>> lamda) : base(left, right, lamda)
        {
        }

        public override bool Result(decimal left, decimal right)
        {
            return left.CompareTo(right) < 0;
        }
    }
}