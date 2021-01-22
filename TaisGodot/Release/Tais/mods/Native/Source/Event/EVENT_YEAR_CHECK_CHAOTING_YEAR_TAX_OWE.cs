using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tais.API;
using static Tais.API.Method;

namespace Native
{
    public class EVENT_YEAR_CHECK_CHAOTING_YEAR_TAX_OWE: EventDef
    {
        public override VaildDate date => VAILID_DATE(null, 12, 30);

        public override ConditionDef trigger => GREATER(CHAOTING_YEAR_TAX_OWE, 0);

        public override IDesc title => DESC("EVENT_YEAR_CHECK_CHAOTING_YEAR_TAX_OWE_TITLE", 100);

        public override IDesc desc => DESC("EVENT_YEAR_CHECK_CHAOTING_YEAR_TAX_OWE_DESC");

        public override OptionDef[] options => ARRAY(
                EVENT_OPTION(
                    DESC("EVENT_TEST_OPTION1_DESC"),
                    ARRAY(
                        ASSIGN(GM_END, true)
                    )
                ),
                EVENT_OPTION(
                    DESC("EVENT_TEST_OPTION2_DESC")
                )
            );

        
    }
}
