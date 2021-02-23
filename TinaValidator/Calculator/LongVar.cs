using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as NumberConst;
        public LongVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, parent, keys, source)
        { }
    }
}
