using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public abstract class IOption
    {
        public IDesc desc;
        public IOperation[] operations;
    }
}
