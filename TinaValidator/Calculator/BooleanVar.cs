using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanVar : Variable, IBoolean
    {
        public BooleanConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as BooleanConst;
        public BooleanVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IBoolean);
    }
}
