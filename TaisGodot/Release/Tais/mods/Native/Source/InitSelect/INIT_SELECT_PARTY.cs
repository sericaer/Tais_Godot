using Native.Party;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais;
using Tais.API;
using static Tais.API.Method;
using static Tais.API.VisitorGroup;
namespace Native.InitSelect
{
    public class INIT_SELECT_PARTY : Tais.API.InitSelect
    {
        public INIT_SELECT_PARTY()
        {
             
            title = DESC("INIT_SELECT_PARTY_TITLE");
            desc  = DESC("INIT_SELECT_PARTY_DESC");

            options = ARRAY
            (
                OPTION_INIT_SELECT
                (
                    DESC("INIT_SELECT_PARTY_OPTION_1_DESC"),
                    ARRAY
                    (
                        ASSIGN(INIT_PARTY, typeof(XUNGUI))
                    )
                )
            ) ;

            IsFirst = true;
        }
 
    }
}
