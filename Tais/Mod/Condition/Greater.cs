using System;
using System.Linq.Expressions;
using Tais.API;

namespace Tais.Mod.Condition
{
    internal class Greater : ConditionDef
    {
        public IVisitorR<decimal> left;

        public IVisitorR<decimal> right;

        public decimal rightConst;

        public Expression<Func<decimal, decimal>> lamda;

        public Greater(IVisitorR<decimal> left, decimal right)
        {
            this.left = left;
            this.rightConst = right;
        }

        public Greater(IVisitorR<decimal> left, IVisitorR<decimal> right, Expression<Func<decimal, decimal>> lamda)
        {
            this.left = left;
            this.right = right;
            this.lamda = lamda;
        }

        public bool isTrue()
        {
            decimal rightValue = rightConst;

            if (right != null)
            {
                rightValue = right.GetValue();
            }
            if (lamda != null)
            {
                rightValue = lamda.Compile().Invoke(rightValue);
            }

            return left.GetValue().CompareTo(rightValue) > 0;
        }
    }
}