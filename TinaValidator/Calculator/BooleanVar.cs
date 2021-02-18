using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanVar : Variable, IBoolean
    {
        public BooleanConst GetResult(IVariableLinker vl)   
            => new BooleanConst(Convert.ToBoolean(vl.GetValue(this)));
        public BooleanVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
    }
}
