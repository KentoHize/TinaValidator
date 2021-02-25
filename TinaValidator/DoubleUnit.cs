using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : Unit
    {
        public static DoubleUnit Float = new DoubleUnit(float.MinValue, float.MaxValue);        
        public double Value1 { get => _Value1; set => _Value1 = value != double.NaN ? value : throw new ArgumentException(nameof(Value1)); }
        private double _Value1; //min exact
        public double Value2 { get => _Value2; set => _Value2 = value != double.NaN ? value : throw new ArgumentException(nameof(Value2)); }
        private double _Value2; //max
        public CompareMethod CompareMethod { get; set; }
        public double[] Select
        {
            get => _Select;
            set
            {
                if (value != null)
                {
                    for (long i = 0; i < value.Length; i++)
                        if (value[i] == double.NaN)
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private double[] _Select;

        public DoubleUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = compareMethod;
        }

        public DoubleUnit(CharsToDoublePart ctdp)
        {
            CompareMethod = ctdp.CompareMethod;
            Value1 = ctdp.Value1;
            Value2 = ctdp.Value2;
        }
        public DoubleUnit(double exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }

        public DoubleUnit(double minValue, double maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }
        public DoubleUnit(double[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }

        public override bool Compare(object o)
        {
            if (!double.TryParse(o.ToString(), out double d))
                return false;
            else if (d == double.NaN)
                return false;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return _Value1 == d;
                case CompareMethod.Not:
                    return _Value1 != d;
                case CompareMethod.MinMax:
                    return d >= _Value1 && d <= _Value2;
                case CompareMethod.NotMinMax:
                    return d < _Value1 || d > _Value2;
                case CompareMethod.Select:
                    if (_Select == null)
                        return false;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i] == d)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (_Select == null)
                        return true;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i] == d)
                            return false;
                    return true;
                case CompareMethod.Special:
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return _Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            double d;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return rnd.NextRandomDouble();
                case CompareMethod.Not:
                    do { d = rnd.NextRandomDouble(); }
                    while (d == _Value1);
                    return d;
                case CompareMethod.MinMax:
                    if (_Value1 > _Value2)
                        throw new ArgumentException(nameof(Value1) + nameof(Value2));
                    return rnd.NextRandomDouble(_Value1, _Value2);
                case CompareMethod.NotMinMax:
                    //Scan
                    double rd = (double)rnd.NextDouble() * Math.Abs(_Value1 - _Value2);
                    if (rd < _Value1)
                        return rd;
                    else
                        return (_Value2 + rd - _Value1 + double.MinValue);
                case CompareMethod.Select:
                    if (_Select == null || _Select.Length == 0)
                        return null;
                    return _Select[rnd.Next(_Select.Length)];
                case CompareMethod.NotSelect:
                    if (_Select == null || _Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {
                        d = rnd.NextRandomDouble();
                        for (int i = 0; i < _Select.Length; i++)
                            if (_Select[i] == d)
                                continue;
                        return d;
                    }
                case CompareMethod.Special:
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}
