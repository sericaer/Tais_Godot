﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public interface IDepart
    {
        (int r, int g, int b) color { get; }
    }
}
