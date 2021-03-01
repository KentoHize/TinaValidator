using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class CharConst : NumberConst
    {
        private char _Value;
        public char Value => _Value;
        public CharConst(char value)
            => _Value = value;        
        public override StringConst ToStringConst()
            => new StringConst(_Value);

        public static implicit operator char(CharConst a) => a._Value;
        public override string ToString()
         => _Value.ToString();
        public override BooleanConst EqualTo(CharConst b)
            => new BooleanConst(_Value == b._Value);
        public override BooleanConst LessThan(CharConst b)
            => new BooleanConst(_Value < b._Value);
        public override BooleanConst GreaterThan(CharConst b)
            => new BooleanConst(_Value > b._Value);
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (obj is null)
                return false;
            if (!(obj is CharConst c))
                return false;
            return c.EqualTo(this);
        }
        public override int GetHashCode()
            => _Value.GetHashCode();
        public override object Clone()
            => new CharConst(_Value);
    }
}
