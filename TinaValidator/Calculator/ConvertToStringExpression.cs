using System;

namespace Aritiafel.Artifacts.Calculator
{
    public class ConvertToStringExpression : Expression, IString
    {
        public IObject A { get; set; }
        public ConvertToStringExpression(IObject a = null)
            => A = a;
        public override ObjectConst GetObject(IVariableLinker vl)
            => GetResult(vl);
        public override Type GetObjectType()
            => typeof(IString);
        public StringConst GetResult(IVariableLinker vl)
            => A.GetObject(vl).ToStringConst();
        public override object Clone()
            => new CompareExpression((IObject)A.Clone());
    }
}
