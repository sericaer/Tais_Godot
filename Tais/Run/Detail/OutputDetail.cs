using System.Collections.Generic;
using System.Linq;

namespace Tais.Run
{
    class OutputDetail : Detail
    {
        public enum TYPE
        {
            CHAOTING_YEAR_TAX
        }

        public decimal this[TYPE type] => (subs.Single(x => x.name == type.ToString()) as Detail_Leaf).value;

        public OutputDetail(string name) : base(name)
        {

        }
    }
}