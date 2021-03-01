using System;
using Aritiafel.Artifacts.Calculator;

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
        //public static IntegerUnit UnsignedLong => new IntegerUnit(ulong.MinValue, ulong.MaxValue);
        public CompareMethod CompareMethod { get; set; }
        public INumber Value1
        {
            get => _Value1;
            set => _Value1 = value is LongConst || value is LongVar ?
                value : throw new ArgumentException(nameof(Value1));
        }
        private INumber _Value1; //min exact
        public INumber Value2
        {
            get => _Value2;
            set => _Value2 = value is LongConst || value is LongVar ?
                value : throw new ArgumentException(nameof(Value2));
        }
        private INumber _Value2; //max
        public INumber[] Select
        {
            get => _Select;
            set
            {
                if (value != null)
                {
                    for (long i = 0; i < value.Length; i++)
                        if (!(value[i] is LongConst || value[i] is LongVar))
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private INumber[] _Select;
        public IntegerUnit()
            : this(CompareMethod.Any)
        { }

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
        public IntegerUnit(long exactValue, CompareMethod compareMethod = CompareMethod.Exact)
            : this(new IntegerUnit(exactValue) as INumber, compareMethod)
        { }
        public IntegerUnit(INumber exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }
        public IntegerUnit(long minValue, long maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
            : this(new IntegerUnit(minValue) as INumber, new IntegerUnit(maxValue) as INumber, compareMethod)
        { }
        public IntegerUnit(INumber minValue, INumber maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }
        public IntegerUnit(long[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            INumber[] array = new INumber[select.Length];
            for (long i = 0; i < select.Length; i++)
                array[i] = new LongConst(select[i]);
            Select = array;
        }
        public IntegerUnit(INumber[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }
        public override bool Compare(ObjectConst o, IVariableLinker vl = null)
        {
            if (!(o is LongConst l))
                return false;          

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return _Value1.GetResult(vl) == l;
                case CompareMethod.Not:
                    return _Value1.GetResult(vl) != l;
                case CompareMethod.MinMax:
                    return l >= _Value1.GetResult(vl) && l <= _Value2.GetResult(vl);
                case CompareMethod.NotMinMax:
                    return l < _Value1.GetResult(vl) || l > _Value2.GetResult(vl);
                case CompareMethod.Select:
                    if (_Select == null)
                        return false;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i].GetResult(vl) == l)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (_Select == null)
                        return true;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i].GetResult(vl) == l)
                            return false;
                    return true;
                case CompareMethod.Special:
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override ObjectConst Random(IVariableLinker vl = null)
        {
            if (CompareMethod == CompareMethod.Exact)
                return _Value1.GetResult(vl);
            ChaosBox cb = new ChaosBox();            
            LongConst l;

            switch (CompareMethod)
            {   
                case CompareMethod.Any:
                    return new LongConst(cb.DrawOutLong());
                case CompareMethod.Not:
                    do { l = new LongConst(cb.DrawOutLong()); }
                    while (l == _Value1.GetResult(vl));
                    return l;
                case CompareMethod.MinMax:
                    return new LongConst(cb.DrawOutLong(
                        (long)_Value1.GetResult(vl).Value,
                        (long)_Value2.GetResult(vl).Value));
                 case CompareMethod.NotMinMax:
                    //Scan
                    l = new LongConst((long)Math.Round(cb.DrawOutDouble(0, 1) * Math.Abs((long)(_Value1.GetResult(vl) - _Value2.GetResult(vl)).Value)));
                    if (l < _Value1.GetResult(vl))
                        return l;
                    else
                        return _Value2.GetResult(vl) + l - _Value1.GetResult(vl) + LongConst.MinValue;
                case CompareMethod.Select:
                    if (_Select == null || _Select.Length == 0)
                        return null;
                    return _Select[cb.DrawOutInteger(0, _Select.Length - 1)].GetResult(vl);
                case CompareMethod.NotSelect:
                    if (_Select == null || _Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {
                        l = new LongConst(cb.DrawOutLong());
                        for (int i = 0; i < _Select.Length; i++)
                            if (_Select[i].GetResult(vl) == l)
                                continue;
                        return l;
                    }
                case CompareMethod.Special:
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}



