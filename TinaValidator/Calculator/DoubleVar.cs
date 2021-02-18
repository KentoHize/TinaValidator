using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => new DoubleConst((double)vl.GetValue(this));
        public DoubleVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
    }
}
