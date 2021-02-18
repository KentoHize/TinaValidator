using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class BooleanConst : IBoolean
    {
        private bool _Value;
        public bool Value => _Value;
        public BooleanConst(bool value)
            => _Value = value;
        public BooleanConst GetResult(IVariableLinker vl)
            => this;
        public static bool operator true(BooleanConst a)
            => true;
        public static bool operator false(BooleanConst a)
            => false;
        public static BooleanConst operator &(BooleanConst a, BooleanConst b)
          => a.And(b);
        public static BooleanConst operator |(BooleanConst a, BooleanConst b)
          => a.Or(b);
        public static BooleanConst operator !(BooleanConst a)
          => a.Not();
        public BooleanConst And(BooleanConst b)
            => new BooleanConst(_Value && b._Value);
        public BooleanConst Or(BooleanConst b)
            => new BooleanConst(_Value || b._Value);
        public BooleanConst Not()
            => new BooleanConst(!_Value);        
    }
}
