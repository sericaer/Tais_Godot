using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Mod.Condition
{
    public class Equal<T> : ConditionDef
    {
        public IVisitorR<T> left;

        public IVisitorR<T> right;

        public T rightConst;

        public Equal(IVisitorR<T> left, T right)
        {
            this.left = left;
            this.rightConst = right;
        }

        public Equal(IVisitorR<T> left, IVisitorR<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public bool isTrue()
        {
            if(right != null) 
            {
                return EqualityComparer<T>.Default.Equals(left.GetValue(), right.GetValue());
            }
            else
            {
                return EqualityComparer<T>.Default.Equals(left.GetValue(), rightConst);
            }
        }
    }
}
