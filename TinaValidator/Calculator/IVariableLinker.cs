using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IVariableLinker
    {
        object GetValue(Variable v);
        void SetValue(Variable v, object value);
    }
}
