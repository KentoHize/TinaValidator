using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class NumberVar : Variable, INumber
    {
        public abstract NumberConst GetResult(IVariableLinker vl);
        protected NumberVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(INumber);
    }
}
