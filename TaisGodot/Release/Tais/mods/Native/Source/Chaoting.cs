using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;
using static Tais.API.Method;

namespace Native
{
    public class Chaoting : ChaotingDef
    {
        public override decimal[] taxRates => ARRAY(
            0.001M,
            0.002M,
            0.003M,
            0.0045M,
            0.006M,
            0.0075M,
            0.009M
            );

        public override PowerStatus[] powerStatusArray => new PowerStatus[]
        {
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (0, 9), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 10} }
        };
    }
}
