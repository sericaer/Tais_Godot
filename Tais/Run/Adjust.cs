using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    enum ADJUST_TYPE
    {
        POP_TAX,
        CHAOTING_TAX,
    }

    interface IAdjust : INotifyPropertyChanged
    {
        ADJUST_TYPE type { get; }

        int percent { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Adjust : IAdjust
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        [JsonProperty]
        public ADJUST_TYPE type { get; set; }

        [JsonProperty]
        public int percent { get; set; }

        public Adjust(AdjustDef def)
        {
            type = def.type;

            percent = def.init_percent;
        }

        [JsonConstructor]
        public Adjust()
        {

        }
    }
}
