using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.Visitor;

namespace Tais.API
{
    public static class VisitorGroup
    {
        //public readonly static IVisitor GM_A = new GMVisitor<Run.Runner>(x => x.a);

        public readonly static IVisitor INIT_PARTY = HelperClass<Init.Initer>.Property(x => x.party); 
    }

    public interface IVisitor
    {
        void SetValue(object value);
    }


}
