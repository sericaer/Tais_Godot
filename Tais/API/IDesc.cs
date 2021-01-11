using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public interface IDesc
    {
        string format { get; }
        object[] objs { get; }
    }

    class Desc : IDesc
    {
        public string format { get; set; }

        public object[] objs { get; set; }
    }
}
