﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tais.API;
using static Tais.API.Method;
using static Tais.API.VisitorGroup;

namespace Native
{

    public class GEventTest : IEvent
    {
        public IOperation op => null;//ASSIGN(GM_A, 999);
    }
}
