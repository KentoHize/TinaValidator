using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class ConvertToStringExpression : IString, IObject
    {
        public IObject A { get; set; }
        public ConvertToStringExpression(IObject a = null)
            => A = a;
        public ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public Type GetObjectType()
            => typeof(IString);
        public StringConst GetResult(IVariableLinker vl)
            => A.GetObject(vl).ToStringConst();
    }
}
