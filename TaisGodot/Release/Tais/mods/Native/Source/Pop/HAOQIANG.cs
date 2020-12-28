using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Native.Pop
{
    class HAOQIANG : IPop
    {
        public decimal num { get; set; }

        public bool is_tax => true;
    }
}
