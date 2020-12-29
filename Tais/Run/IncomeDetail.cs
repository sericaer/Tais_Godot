using System;
using System.Collections.Generic;
using System.Linq;

namespace Tais.Run
{
    class IncomeDetail : Detail
    {
        public enum TYPE
        {
            POP_TAX
        }

        public IEnumerable<IDetail> this[TYPE type] => (subs.Single(x=>x.name == type.ToString()) as Detail).subs;


        public IncomeDetail(string name) : base(name)
        {

        }

        public decimal Sum()
        {
            return subs.Sum(x => x.value);
        }

        public void Update(TYPE type, IEnumerable<Detail_Leaf> leafs)
        {
            var sub = subs.Find(x => x.name == type.ToString());
            if(sub != null)
            {
                var subDetail = sub as Detail;
                if(subDetail == null)
                {
                    throw new Exception();
                }

                subDetail.subs.Clear();
                subDetail.subs.AddRange(leafs);
                return;
            }

            subs.Add(new Detail(type.ToString(), leafs));
        }
    }
}