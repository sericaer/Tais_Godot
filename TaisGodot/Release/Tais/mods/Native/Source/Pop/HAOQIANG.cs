using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Native.Pop
{
    class HAOQIANG : IPopDef
    {
        public decimal num { get; set; }

        public bool is_tax => true;
    }
}
