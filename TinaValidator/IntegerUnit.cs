using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public enum CompareMethod
    {
        Any = 0,        
        Exact = 1,
        Not = 2,
        MinMax = 3,
        NotMinMax = 4,
        Select = 5,
        NotSelect = 6,
        Special = 7
    }

    public class IntegerUnit : Unit
    {
        public static IntegerUnit Byte => new IntegerUnit(byte.MinValue, byte.MaxValue);
        public static IntegerUnit Short => new IntegerUnit(short.MinValue, short.MaxValue);
        public static IntegerUnit UnsignedShort => new IntegerUnit(ushort.MinValue, ushort.MaxValue);
        public static IntegerUnit Int => new IntegerUnit(int.MinValue, int.MaxValue);
        public static IntegerUnit UnsignedInt => new IntegerUnit(uint.MinValue, uint.MaxValue);
        public static IntegerUnit Long => new IntegerUnit(long.MinValue, long.MaxValue);
        public static IntegerUnit UnsignedLong => new IntegerUnit(ulong.MinValue, ulong.MaxValue);
        public CompareMethod CompareMethod { get; set; }
        public decimal Value1 { get => _Value1; set => _Value1 = Math.Ceiling(value) == value ? value : throw new ArgumentException(nameof(Value1)); }
        private decimal _Value1; //min exact
        public decimal Value2 { get => _Value2; set => _Value2 = Math.Ceiling(value) == value ? value : throw new ArgumentException(nameof(Value2)); }
        private decimal _Value2; //max
        public decimal[] Select
        {
            get => _Select;
            set
            {
                if (value != null)
                {
                    for (long i = 0; i < value.Length; i++)
                        if (Math.Ceiling(value[i]) != value[i])
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private decimal[] _Select;

        public IntegerUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = compareMethod;
        }   

        public IntegerUnit(CharsToIntegerPart ctip)
        {
            CompareMethod = ctip.CompareMethod;
            Value1 = ctip.Value1;
            Value2 = ctip.Value2;
        }

        public IntegerUnit(decimal exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }

        public IntegerUnit(decimal minValue, decimal maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }
        public IntegerUnit(decimal[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }

        public override bool Compare(object b)
        {
            if (!decimal.TryParse(b.ToString(), out decimal d))
                return false;
            else if (Math.Round(d) != d) // Not Integer
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
            decimal d;

            switch (CompareMethod)
            {   
                case CompareMethod.Any:
                    return new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0);
                case CompareMethod.Not:
                    do { d = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0); }
                    while (d == _Value1);
                    return d;
                case CompareMethod.MinMax:
                    if (_Value1 > _Value2)
                        throw new ArgumentException(nameof(Value1) + nameof(Value2));
                    return Math.Round((decimal)rnd.NextDouble() * (Value2 - Value1) + Value1);
                case CompareMethod.NotMinMax:
                    //Scan
                    decimal ri = (decimal)rnd.NextDouble() * (_Value1 - decimal.MinValue + decimal.MaxValue - _Value2);
                    if (ri < _Value1)
                        return ri;
                    else
                        return (_Value2 + ri - _Value1 + decimal.MinValue);
                case CompareMethod.Select:
                    if (_Select == null || _Select.Length == 0)
                        return null;
                    return _Select[rnd.Next(_Select.Length)];
                case CompareMethod.NotSelect:
                    if (_Select == null || _Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {
                        d = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0);
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



