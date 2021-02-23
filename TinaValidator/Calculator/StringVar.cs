using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringVar : Variable, IString
    {
        public StringConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as StringConst;
        public StringVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, parent, keys, source)
        { }

        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);
    }
}
