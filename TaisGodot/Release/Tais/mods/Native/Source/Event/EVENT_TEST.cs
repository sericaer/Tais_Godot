using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Tais.API;
using static Tais.API.Method;

namespace Native
{
    public class EVENT_TEST : EventDef
    {
        public override VaildDate date => VAILID_DATE(null, null, 10);

        public override ConditionDef trigger => LESS(CHAOTIN_YEAR_TAX_DIFF, 0);

        public override IDesc title => DESC("EVENT_TEST_TITLE", INIT_PARTY);

        public override IDesc desc => DESC("EVENT_TEST_DESC");

        public override OptionDef[] options => ARRAY(
                EVENT_OPTION(
                    DESC("EVENT_TEST_OPTION1_DESC")
                ),
                EVENT_OPTION(
                    DESC("EVENT_TEST_OPTION2_DESC")
                )
            );

        
    }
}
