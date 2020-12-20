using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaisGodot.Scripts;

namespace TaisGodot.Scripts
{
    class UserSetting
    {
        public static string lang
        {
            get
            {
                return TranslateServerEx.GetLocale();
            }
            set
            {
                TranslateServerEx.SetLocale(value);
            }
        }
    }
}
