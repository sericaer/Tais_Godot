using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Visitor;

namespace Tais.API
{
    public static class VisitorGroup
    {
        public readonly static IVisitor GM_A = new GMVisitor(typeof(Run.Runner), "a");

        public readonly static IVisitor INIT_PARTY = new GMVisitor(typeof(Init.Initer), "party");
    }

    public interface IVisitor
    {
        void SetValue(object value);
    }
}
