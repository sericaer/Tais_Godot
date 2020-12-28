using Native.Pop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using static Tais.API.Method;
namespace Native.Depart
{
    public class DEPART_JIXIAN : IDepart
    {
        public Color color => (63,72,204);

        public IPop[] pops => ARRAY(POP_NUM<HAOQIANG>(1000));
    }
}
