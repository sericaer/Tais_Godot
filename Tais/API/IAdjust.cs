﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tais.API
{
    public interface IAdjust
    {
        decimal[] level_rates { get; }

        int init_level { get; }
    }

    public interface IAdjustTax : IAdjust
    {

    }
}