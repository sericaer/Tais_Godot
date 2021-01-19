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

        decimal expectYearTax { get; set; }

        decimal reportYearTax { get; set; }

        decimal yearTaxOwe { get; }

        decimal yearTaxExcess { get; }

        OutputDetail outputDetail { get; }

        void UpdateReportTaxPercent(int percent);
        void DaysInc((int y, int m, int d) date);
    }

    class Chaoting : IChaoting
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067
        
        [JsonProperty]
        public int reportPopNum { get; set; }

        [JsonProperty]
        public int currTaxLevel { get; set; }

        [JsonProperty]
        public decimal[] taxRates { get; set; }

        [JsonProperty]
        public decimal expectYearTax { get; set; }

        [JsonProperty]
        public decimal reportYearTax { get; set; }

        public decimal reportRate { get; set; }

        public decimal expectTax => taxRates[currTaxLevel-1] * reportPopNum;

        public decimal reportTax => expectTax * reportRate / 100;

        public decimal yearTaxExcess => reportYearTax > expectYearTax ? reportYearTax - expectYearTax : 0;

        public decimal yearTaxOwe => reportYearTax < expectYearTax ? expectYearTax - reportYearTax : 0;


        public OutputDetail outputDetail { get { return _outputDetail; } set { _outputDetail = value; } }

        private OutputDetail _outputDetail;

        public static Chaoting Gen(ChaotingDef def, int initTaxLevel, int reportPopNum)
        {
            var inst = new Chaoting();
            inst.taxRates = def.taxRates;
            inst.currTaxLevel = initTaxLevel;
            inst.reportPopNum = reportPopNum;

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

        public void DaysInc((int y, int m, int d) date)
        {
            if(date.d == 30)
            {
                expectYearTax += expectTax;
                reportYearTax += reportTax;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(expectYearTax)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(reportYearTax)));
            }
        }
    }
}