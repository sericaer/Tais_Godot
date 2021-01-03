using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tais.Run
{
    [JsonObject(MemberSerialization.OptIn)]
    interface IChaoting : INotifyPropertyChanged
    {
        [JsonProperty]
        int reportPopNum { get; }

        [JsonProperty]
        decimal yearTaxRate { get; }

        OutputDetail outputDetail { get; }
    }

    class Chaoting : IChaoting
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int reportPopNum { get; set; }
        public decimal yearTaxRate { get; set; }

        public OutputDetail outputDetail => _outputDetail;

        private OutputDetail _outputDetail;

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            _outputDetail = new OutputDetail("CBAOTING_TAX");

            this.OBSProperty(x => x.reportPopNum).Subscribe(_ => UpdateOutput());
            this.OBSProperty(x => x.yearTaxRate).Subscribe(_ => UpdateOutput());
        }

        private void UpdateOutput()
        {
            _outputDetail.Update(OutputDetail.TYPE.CHAOTING_COMMON_TAX, new List<Detail_Leaf>() { new Detail_Leaf() });
        }
    }
}
