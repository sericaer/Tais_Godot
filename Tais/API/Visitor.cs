using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Tais.Visitor;

namespace Tais.API
{
    public interface IVisitor<T> : IVisitorR<T>
    {
        void SetValue(T value);
    }

    public interface IVisitorR<T>
    {
        T GetValue();
    }
}
