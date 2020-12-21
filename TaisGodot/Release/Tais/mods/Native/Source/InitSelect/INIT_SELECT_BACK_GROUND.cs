using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais;
using Tais.API;
using static Tais.API.Method;

namespace Native.InitSelect
{
    public class INIT_SELECT_BACK_GROUND : Tais.API.InitSelect
    {
        public INIT_SELECT_BACK_GROUND()
        {
             
            title = DESC("INIT_SELECT_BACK_GROUND_TITLE");
            desc = DESC("INIT_SELECT_BACK_GROUND_DESC");

            options = ARRAY
            (
                OPTION_INIT_SELECT
                (
                    DESC("INIT_SELECT_BACK_GROUND_OPTION_1_DESC")
                )
            );

            IsFirst = true;
        }
 
    }
}
