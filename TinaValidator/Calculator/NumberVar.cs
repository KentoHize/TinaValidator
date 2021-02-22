using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class NumberVar : Variable, INumber
    {
        public abstract NumberConst GetResult(IVariableLinker vl);
        protected NumberVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(INumber);
    }
}
