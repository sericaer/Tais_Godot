using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public class Color
    {
        public int r;
        public int g;
        public int b;

        public Color(int r, int g, int b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public static implicit operator Color((int r, int g, int b) color)//隐式声明的int转myclass类处理方法
        {
            return new Color(color.r, color.g, color.b);
        }
    }
}
