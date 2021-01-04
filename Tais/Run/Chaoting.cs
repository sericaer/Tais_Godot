using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Tais.API;

namespace Tais.Run
{
    interface IChaoting : INotifyPropertyChanged
    {

        int reportPopNum { get; set; }

        int currTaxLevel { get; set; }

        decimal expectTax { get; } 

        decimal reportTax { get; }


        OutputDetail outputDetail { get; }

        void UpdateReportTaxPercent(int percent);
    }

    class Chaoting : IChaoting
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067

        public int reportPopNum { get; set; }

        public int currTaxLevel { get; set; }

        public decimal[] taxRates { get; set; }

        public decimal reportRate { get; set; }

        public decimal expectTax => taxRates[currTaxLevel] * reportPopNum;

        public decimal reportTax => expectTax * reportRate;

        public OutputDetail outputDetail { get { return _outputDetail; } set { _outputDetail = value; } }

        private OutputDetail _outputDetail;

        public static Chaoting Gen(ChaotingDef def, int initTaxLevel)
        {
            var inst = new Chaoting();
            inst.taxRates = def.taxRates;
            inst.currTaxLevel = initTaxLevel;

            inst.IntegrateData();
            return inst;
        }

        [OnDeserialized]
        public void IntegrateData(StreamingContext context = default(StreamingContext))
        {
            _outputDetail = new OutputDetail("REPORT_CHAOTING");

            this.OBSProperty(x => x.reportTax).Subscribe(_ => UpdateOutputDetail());
        }

        private void UpdateOutputDetail()
        {
            _outputDetail.subs.Clear();
            _outputDetail.subs.Add(new Detail_Leaf(OutputDetail.TYPE.CHAOTING_YEAR_TAX.ToString(), reportTax));

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(outputDetail)));
        }

        public void UpdateReportTaxPercent(int percent)
        {
            reportRate = percent;
        }
    }
}