using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class NumberVar : Variable, INumber
    {
        public abstract NumberConst GetResult(IVariableLinker vl);
        protected NumberVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
    }
}
