using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Mod.Condition
{
    public class Equal<T> : ConditionBase<T>
    {
        public Equal(IVisitorR<T> left, T right) : base(left, right)
        {
            this.left = left;
            this.rightConst = right;
        }

        public Equal(IVisitorR<T> left, IVisitorR<T> right) : base(left, right, null)
        {
            this.left = left;
            this.right = right;
        }

        public override bool Result(T left, T right)
        {
            return EqualityComparer<T>.Default.Equals(right, left);
        }
    }
}
