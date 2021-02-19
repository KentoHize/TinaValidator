using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class NumberVar : Variable, INumber, IObject
    {
        public abstract NumberConst GetResult(IVariableLinker vl);
        protected NumberVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }

        public ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public Type GetObjectType()
            => typeof(INumber);
    }
}
