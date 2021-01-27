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
        public override decimal initPower => 70;

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
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (0, 9), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 1} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (10, 19), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 2} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (20, 29), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 3} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (30, 39), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 4} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (40, 49), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 5} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (50, 59), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 6} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (60, 69), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 7} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (70, 79), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 8} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (80, 89), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 9} },
            new PowerStatus(){ desc = "POWER_STATUS_1", powerRange = (90, 100), effect = new PowerStatus.Effect{ min_chaoting_report_tax_level = 10} },
        };

        
    }
}
