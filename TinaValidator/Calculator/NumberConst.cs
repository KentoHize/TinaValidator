using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class NumberConst : INumber
    {
        public abstract object Value { get; }
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
        {
            if (Math.Round(b) != b)
                return new DoubleConst(b + a);
            double d = b + a;
            if (d >= long.MinValue && d <= long.MaxValue)
                return new LongConst((long)d);
            return new DoubleConst(d);
        }            
        public static NumberConst LongMinusDouble(long a, double b)
        {
            if (Math.Round(b) != b)
                return new DoubleConst(a - b);
            double d = a - b;
            if (d >= long.MinValue && d <= long.MaxValue)
                return new LongConst((long)d);
            return new DoubleConst(d);
        }            
        public static NumberConst DoubleMinusLong(double a, long b)
        {
            if (Math.Round(a) != a)
                return new DoubleConst(a - b);
            double d = a - b;
            if (d >= long.MinValue && d <= long.MaxValue)
                return new LongConst((long)d);
            return new DoubleConst(d);
        }            
        public static NumberConst LongMultiplyDouble(long a, double b)
        {
            decimal m = (decimal)(b * a);
            if (Math.Round(m) == m && m >= long.MinValue && m <= long.MaxValue)
                return new LongConst((long)m);
            return new DoubleConst((double)m);
        }   
        public static NumberConst LongDivideDouble(long a, double b)
        {
            decimal m = a / (decimal)b;
            if (Math.Round(m) == m && m >= long.MinValue && m <= long.MaxValue)
                return new LongConst((long)m);
            return new DoubleConst((double)m);
        }            
        public static NumberConst DoubleDivideLong(double a, long b)
        {
            decimal m = (decimal) a / b;
            if (Math.Round(m) == m && m >= long.MinValue && m <= long.MaxValue)
                return new LongConst((long)m);
            return new DoubleConst((double)m);
        }
        public static NumberConst LongExactlyDivideDouble(long a, double b)
        {
            decimal m = a / (decimal)b;
            if (Math.Round(m) == m && m >= long.MinValue && m <= long.MaxValue)
                return new LongConst((long)Math.Round(m));
            return new DoubleConst((double)Math.Round(m));
        }   
        public static NumberConst LongDivideLong(long a, long b)
        {
            if (a % b == 0)
                return new LongConst(a / b);
            return new DoubleConst((double)a / (double)b);
        }
        public static NumberConst DoubleExactlyDivideLong(double a, long b)
        {
            double d = Math.Round(a / b);
            if (d >= long.MinValue && d <= long.MaxValue)
                return new LongConst((long)d);
            return new DoubleConst((double)d);
        }
        public abstract NumberConst GetResult(IVariableLinker vl);
    }
}
