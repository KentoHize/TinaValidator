using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    
    public abstract class NumberConst : Number
    {   
        public abstract NumberConst Add(NumberConst b);
        public abstract NumberConst Minus(NumberConst b);
        public abstract NumberConst ReverseMinus(NumberConst b);            
        public abstract NumberConst Multiply(NumberConst b);
        public abstract NumberConst Divide(NumberConst b);
        public abstract NumberConst ReverseDivide(NumberConst b);
        public abstract NumberConst Remainder(NumberConst b);
        public abstract NumberConst ReverseRemainder(NumberConst b);
        public static NumberConst operator +(NumberConst a, NumberConst b)
            => a.Add(b);
        public static NumberConst operator -(NumberConst a, NumberConst b)
            => a.Minus(b);
        public static NumberConst operator *(NumberConst a, NumberConst b)
            => a.Multiply(b);
        public static NumberConst operator /(NumberConst a, NumberConst b)
            => a.Divide(b);
        public static NumberConst operator %(NumberConst a, NumberConst b)
            => a.Remainder(b);
        public static NumberConst operator ++(NumberConst a)
            => a + new LongConst(1);
        public static NumberConst operator --(NumberConst a)
            => a - new LongConst(1);
    }
}
