using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.Calculator
{
    public class StringVar : Variable, IString
    {
        public StringVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
        public StringConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as StringConst;
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);
        public override object Clone()
            => new LongVar { Name = Name, Parent = Parent, Keys = Keys, Source = Source, Tag = Tag };
    }
}
