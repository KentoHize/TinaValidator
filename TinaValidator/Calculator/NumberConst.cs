using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    
    public abstract class NumberConst : Number
    {   
        public abstract NumberConst ReverseAdd(NumberConst b);
        public abstract NumberConst Add(LongConst b);
        public abstract NumberConst Add(DoubleConst b);

        public abstract NumberConst ReverseMinus(NumberConst b);
        public abstract NumberConst Minus(LongConst b);
        public abstract NumberConst Minus(DoubleConst b);

        public abstract NumberConst ReverseMultiply(NumberConst b);
        public abstract NumberConst Multiply(LongConst b);
        public abstract NumberConst Multiply(DoubleConst b);

        public abstract NumberConst ReverseDivide(NumberConst b);
        public abstract NumberConst Divide(LongConst b);
        public abstract NumberConst Divide(DoubleConst b);

        public abstract NumberConst ReverseExactlyDivide(NumberConst b);
        public abstract NumberConst ExactlyDivide(LongConst b);
        public abstract NumberConst ExactlyDivide(DoubleConst b);

        public abstract NumberConst ReverseRemainder(NumberConst b);
        public abstract NumberConst Remainder(LongConst b);
        public abstract NumberConst Remainder(DoubleConst b);

        public static NumberConst operator +(NumberConst a, NumberConst b)
            => b.ReverseAdd(a);
        public static NumberConst operator -(NumberConst a, NumberConst b)
            => b.ReverseMinus(a);
        public static NumberConst operator *(NumberConst a, NumberConst b)
            => b.ReverseMultiply(a);
        public static NumberConst operator /(NumberConst a, NumberConst b)
            => b.ReverseDivide(a);
        public static NumberConst operator %(NumberConst a, NumberConst b)
            => b.ReverseRemainder(a);
        public static NumberConst operator ++(NumberConst a)
            => a + new LongConst(1);
        public static NumberConst operator --(NumberConst a)
            => a - new LongConst(1);
        public static NumberConst LongAddDouble(long a, double b)
            => null;
        public static NumberConst LongMinusDouble(long a, double b)
            => null;
        public static NumberConst DoubleMinusLong(long a, double b)
            => null;
        public static NumberConst LongMultiplyDouble(long a, double b)
            => null;
        public static NumberConst LongDivideDouble(long a, double b)
            => null;
        public static NumberConst DoubleDivideLong(long a, double b)
            => null;
        public static NumberConst LongExactlyDivideDouble(long a, double b)
            => null;
        public static NumberConst LongDivideLong(long a, long b)
            => null;

        public abstract object Value { get; }
    }
}
