using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class CharVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => vl.GetValue(this) as NumberConst;
        public override object Clone()
            => new CharVar { Name = Name, Parent = Parent, Keys = Keys, Source = Source, Tag = Tag };
        public CharVar(string name = null, object parent = null, List<object> keys = null, VariableSource source = VariableSource.UserVariable)
            : base(name, parent, keys, source)
        { }
    }
}
