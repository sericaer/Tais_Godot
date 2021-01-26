using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
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

        int level { get; set; }

        int percent { get; }

        int min_level { get; set; }

        void UpdateMinLevel(int obj);
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
        public int level { get; set; }

        [JsonProperty]
        public int min_level { get; set; }

        public int percent => level * 10;


        public Adjust(AdjustDef def)
        {
            type = def.type;

            level = def.init_level;

            min_level = 1;

            IntegrateData();
        }

        [JsonConstructor]
        public Adjust()
        {

        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            this.OBSProperty(x => x.min_level).Subscribe(min =>
            {
                if (level < min)
                {
                    level = min;
                }
            });
        }

        public void UpdateMinLevel(int level)
        {
            min_level = level;
            LOG.INFO("level", min_level);
        }
    }
}
