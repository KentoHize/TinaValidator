using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => new LongConst((long)vl.GetValue(this));
        public LongVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
    }
}
