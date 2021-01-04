using Native.Party;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais;
using Tais.API;

namespace Native.InitSelect
{
    public class INIT_SELECT_PARTY : Tais.API.InitSelect
    {

        public override IDesc title => DESC("INIT_SELECT_PARTY_TITLE");

        public override IDesc desc => DESC("INIT_SELECT_PARTY_DESC");

        public override InitSelectOption[] options => ARRAY
            (
                OPTION_INIT_SELECT
                (
                    DESC("INIT_SELECT_PARTY_OPTION_1_DESC"),
                    ARRAY
                    (
                        ASSIGN(INIT_PARTY, typeof(XUNGUI))
                    ),
                    NEXT
                    (
                        typeof(INIT_SELECT_CHAOTING_TAX_LEVEL)
                    )
                )
            );

        public override bool IsFirst => true;
    }
}
