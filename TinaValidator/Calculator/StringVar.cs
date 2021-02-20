using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringVar : Variable, IString
    {
        public StringConst GetResult(IVariableLinker vl)
            => new StringConst(vl.GetValue(this).ToString());
        public StringVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }

        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);
    }
}
