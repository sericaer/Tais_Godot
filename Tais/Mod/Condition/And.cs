using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Mod.Condition
{
    class And : ConditionBase<Boolean>
    {
        public And(IVisitor<bool> left, bool right) : base(left, right)
        {
        }

        public And(IVisitorR<bool> left, IVisitorR<bool> right, Expression<Func<bool, bool>> lamda) : base(left, right, lamda)
        {
        }

        public override bool Result(bool left, bool right)
        {
            return left && right;
        }
    }

    class AndGroup : ConditionDef
    {
        public ConditionDef left;
        public ConditionDef right;

        public AndGroup(ConditionDef left, ConditionDef right)
        {
            this.left = left;
            this.right = right;
        }

        public bool isTrue()
        {
            return left.isTrue() && right.isTrue();
        }
    }
}
