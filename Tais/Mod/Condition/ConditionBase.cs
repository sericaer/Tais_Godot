using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Mod.Condition
{
    public abstract class ConditionBase<T> : ConditionDef
    {
        public IVisitorR<T> left;

        public IVisitorR<T> right;

        public T rightConst;

        public Expression<Func<T, T>> lamda;

        public ConditionBase(IVisitorR<T> left, T right)
        {
            this.left = left;
            this.rightConst = right;
        }

        public ConditionBase(IVisitorR<T> left, IVisitorR<T> right, Expression<Func<T, T>> lamda)
        {
            this.left = left;
            this.right = right;
            this.lamda = lamda;
        }

        public virtual bool isTrue()
        {
            T rightValue = rightConst;

            if (right != null)
            {
                rightValue = right.GetValue();
            }
            if (lamda != null)
            {
                rightValue = lamda.Compile().Invoke(rightValue);
            }

            return Result(left.GetValue(), rightValue);
        }

        public abstract bool Result(T left, T right);
    }
}
