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
        string[] objs { get; }
    }

    class Desc : IDesc
    {
        public string format { get; set; }

        public string[] objs { get; set; }
    }
}
