//using DataVisit;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Runtime.Serialization;

namespace Tais.Run
{
    interface IDate : INotifyPropertyChanged
    {
        (int y, int m, int d) value { get; }

        void Inc();
    }

    [JsonObject(MemberSerialization.OptIn)]
    class Date  : IDate
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

        private int total { get; set; }

        [JsonProperty]
        public int year { get { return total / 360 + 1; } set { total = (value - 1) * 360 + (month - 1) * 30 + day -1; } }

        [JsonProperty]
        public int month { get { return (total % 360) / 30 + 1; } set { total = (year - 1) * 360 + (value - 1) * 30 + day -1; } }

        [JsonProperty]
        public int day { get { return total % 30 + 1; } set { total = (year - 1) * 360 + (month - 1) * 30 + value - 1; } }

        public (int y, int m, int d) value => (year, month, day);

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

            //DataAssociate(new StreamingContext());
        }

        public void Inc()
        {
            total++;
        }

        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}

        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //[OnDeserialized]
        //internal void DataAssociate(StreamingContext context)
        //{
        //}

        //[JsonConstructor]
        //private Date()
        //{
        //}
    }
}