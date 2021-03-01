using System;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : Unit
    {
        public static DoubleUnit Float = new DoubleUnit(float.MinValue, float.MaxValue);        
        public INumber Value1 { get => _Value1;
            set => _Value1 = value is DoubleConst || value is DoubleVar ?
                value : throw new ArgumentException(nameof(Value1)); }
        private INumber _Value1; //min exact
        public INumber Value2 { get => _Value2;
            set => _Value2 = value is DoubleConst || value is DoubleVar ?
                value : throw new ArgumentException(nameof(Value2)); }
        private INumber _Value2; //max
        public CompareMethod CompareMethod { get; set; }
        public INumber[] Select
        {
            get => _Select;
            set
            {
                if (value != null)
                {
                    for (long i = 0; i < value.Length; i++)
                        if (!(value[i] is DoubleConst || value[i] is DoubleVar))
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private INumber[] _Select;
        public DoubleUnit()
            : this(CompareMethod.Any)
        { }

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
            : this(new DoubleConst(exactValue) as INumber, compareMethod)
        { }        
        public DoubleUnit(INumber exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }

        public DoubleUnit(double minValue, double maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
            : this(new DoubleConst(minValue) as INumber, new DoubleConst(maxValue) as INumber, compareMethod)
        { }
        public DoubleUnit(INumber minValue, INumber maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public DoubleUnit(double[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            INumber[] array = new INumber[select.Length];
            for (long i = 0; i < select.Length; i++)
                array[i] = new DoubleConst(select[i]);
            Select = array;
        }
        public DoubleUnit(INumber[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }

        public override bool Compare(ObjectConst o, IVariableLinker vl = null)
        {
            if (!(o is DoubleConst d))
                return false;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return _Value1.GetResult(vl) == d;
                case CompareMethod.Not:
                    return _Value1.GetResult(vl) != d;
                case CompareMethod.MinMax:
                    return d >= _Value1.GetResult(vl) && d <= _Value2.GetResult(vl);
                case CompareMethod.NotMinMax:
                    return d < _Value1.GetResult(vl) || d > _Value2.GetResult(vl);
                case CompareMethod.Select:
                    if (_Select == null)
                        return false;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i].GetResult(vl) == d)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (_Select == null)
                        return true;
                    for (int i = 0; i < _Select.Length; i++)
                        if (_Select[i].GetResult(vl) == d)
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
            Random rnd = new Random((int)DateTime.Now.Ticks);
            DoubleConst d;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return new DoubleConst(rnd.NextRandomDouble());
                case CompareMethod.Not:
                    do { d = new DoubleConst(rnd.NextRandomDouble()); }
                    while (d == _Value1.GetResult(vl));
                    return d;
                case CompareMethod.MinMax:
                    if (_Value1.GetResult(vl) > _Value2.GetResult(vl))
                        throw new ArgumentException(nameof(Value1) + nameof(Value2));
                    return new DoubleConst(rnd.NextRandomDouble((double)_Value1.GetResult(vl).Value,
                        (double)_Value2.GetResult(vl).Value));
                case CompareMethod.NotMinMax:
                    //Scan
                    DoubleConst rd = new DoubleConst((double)rnd.NextDouble() * Math.Abs((double)(_Value1.GetResult(vl) - _Value2.GetResult(vl)).Value));
                    if (rd < _Value1.GetResult(vl))
                        return rd;
                    else
                        return _Value2.GetResult(vl) + rd - _Value1.GetResult(vl) + DoubleConst.MinValue;
                case CompareMethod.Select:
                    if (_Select == null || _Select.Length == 0)
                        return null;
                    return _Select[rnd.Next(_Select.Length)].GetResult(vl);
                case CompareMethod.NotSelect:
                    if (_Select == null || _Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {
                        d = new DoubleConst(rnd.NextRandomDouble());
                        for (int i = 0; i < _Select.Length; i++)
                            if (_Select[i].GetResult(vl) == d)
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
