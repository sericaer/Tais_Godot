﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Native.Pop
{
    class YINHU : PopDef
    {
        public override decimal num { get; set; }

        public override bool is_tax => false;
    }
}
