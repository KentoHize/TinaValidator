using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : Unit
    {
        public CompareMethod CompareMethod { get; set; }
        public double Value1 { get; set; } //min exact
        public double Value2 { get; set; } //max

        public DoubleUnit()
            => CompareMethod = CompareMethod.Any;

        public DoubleUnit(CharsToDoublePart ctdp)
        {
            CompareMethod = ctdp.CompareMethod;
            Value1 = ctdp.Value1;
            Value2 = ctdp.Value2;
        }
        public DoubleUnit(double exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public DoubleUnit(double minValue, double maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public override bool Compare(object b)
        {
            if (!double.TryParse(b.ToString(), out double d))
                return false;
            if (CompareMethod == CompareMethod.Any)
                return true;
            else if (CompareMethod == CompareMethod.Exact)
                return Value1 == d;
            else
                return d >= Value1 && d <= Value2;
        }

        public override object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            if (CompareMethod == CompareMethod.MinMax)
            {
                if (Value1 > Value2)
                    throw new ArgumentException();                
                return rnd.NextRandomDouble(Value1, Value2); 
            }
            else
            {
                double result;
                do
                { // Scan
                    result = rnd.NextRandomDouble();
                } while (result == double.NaN || result == double.NegativeInfinity || result == double.PositiveInfinity);
                return result;
            }
                
        }
    }
}
