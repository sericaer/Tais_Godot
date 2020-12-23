//using DataVisit;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Tais.Run
{
    //[JsonObject(MemberSerialization.OptIn)]
    public class Date  : INotifyPropertyChanged
    {
#pragma warning disable 0067
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore 0067


        //public static bool operator ==(Date l, (int? year, int? month, int? day) r)
        //{
        //    if (r.year != null && l.year != r.year.Value)
        //    {
        //        return false;
        //    }
        //    if (r.month != null && l.month != r.month.Value)
        //    {
        //        return false;
        //    }
        //    if (r.day != null && l.day != r.day.Value)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

        //public static bool operator >(Date l, (int? year, int? month, int? day) r)
        //{
        //    if (r.year != null)
        //    {
        //        if (l.year > r.year.Value)
        //            return true;
        //        if (l.year < r.year.Value)
        //            return false;
        //    }
        //    if (r.month != null)
        //    {
        //        if (l.month > r.month.Value)
        //            return true;
        //        if (l.month < r.month.Value)
        //            return false;
        //    }
        //    if (r.day != null)
        //    {
        //        if (l.day > r.day.Value)
        //            return true;
        //        if (l.day < r.day.Value)
        //            return false;
        //    }

        //    return false;
        //}

        //public static bool operator <(Date l, (int? year, int? month, int? day) r)
        //{
        //    return !(l > r || l == r);
        //}

        //public static bool operator <=(Date l, (int? year, int? month, int? day) r)
        //{
        //    return l < r || l == r;
        //}

        //public static bool operator >=(Date l, (int? year, int? month, int? day) r)
        //{
        //    return l > r || l == r;
        //}

        //public static bool operator !=(Date l, (int? year, int? month, int? day) r)
        //{
        //    return !(l == r);
        //}

        //public static bool operator ==(Date l, Date r)
        //{
        //    return l == (r.year, r.month, r.day);
        //}

        //public static bool operator !=(Date l, Date r)
        //{
        //    return !(l == r);
        //}

        //public static bool operator >(Date l, Date r)
        //{
        //    return l > (r.year, r.month, r.day);
        //}

        //public static bool operator <(Date l, Date r)
        //{
        //    return !(l > r || l == r);
        //}

        //public static bool operator >=(Date l, Date r)
        //{
        //    return l > r || l == r;
        //}

        //public static bool operator <=(Date l, Date r)
        //{
        //    return l < r || l == r;
        //}

        //[JsonProperty, DataVisitorProperty("year")]
        public decimal year { get; set; }

        //[JsonProperty, DataVisitorProperty("month")]
        public decimal month { get; set; }

        //[JsonProperty, DataVisitorProperty("day")]
        public decimal day { get; set; }

        public string desc { get; private set; }

        public decimal total_days { get; private set; }

        //public int total_days
        //{
        //    get
        //    {
        //        return day.Value + (month.Value - 1) * 12 + (year.Value - 1) * 360;
        //    }
        //}

        public Date() //: this()
        {
            year = 1;
            month = 1;
            day = 1;

            DataAssociate(new StreamingContext());
        }

        public void Inc()
        {
            if (day != 30)
            {
                day++;
            }
            else if (month != 12)
            {
                day = 1;
                month++;
                return;
            }
            else
            {
                month = 1;
                day = 1;
                year++;
            }
        }

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        [OnDeserialized]
        internal void DataAssociate(StreamingContext context)
        {
            Observable.Merge(this.OBSProperty(x => x.year),
                           this.OBSProperty(x => x.month),
                           this.OBSProperty(x => x.day))
                .Subscribe(_ =>
                {
                    desc = $"{year}-{month}-{day}";
                    total_days = day + (month - 1) * 30 + (year - 1) * 360;
                });
        }

        //[JsonConstructor]
        //private Date()
        //{
        //}
    }
}