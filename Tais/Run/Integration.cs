using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Tais.Run
{
    interface Integration
    {
        object srcObj { get; }
        object destObj { get; }

        List<(LambdaExpression src, LambdaExpression dest)> binds { get; }
    }

    class IntegrationImp<TS, TD> : Integration where TS : class, INotifyPropertyChanged
    {
        public object srcObj { get; }
        public object destObj { get; }


        public List<(LambdaExpression src, LambdaExpression dest)> binds { get; }

        public IntegrationImp(TS srcObj, TD destObj)
        {
            this.srcObj = srcObj;
            this.destObj = destObj;

            binds = new List<(LambdaExpression src, LambdaExpression dest)>();
        }

        public void With<TP>(Expression<Func<TS, TP>> src, Expression<Func<TD, Action<TP>>> dest)
        {
            binds.Add((src, dest));
            ((TS)srcObj).OBSProperty(src).Subscribe(dest.Compile().Invoke((TD)destObj));
        }
    }
}
