using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as NumberConst;
        public DoubleVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
    }
}
