using System;
using System.Collections.Generic;
using System.Text;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToIntegerPart : Part
    {
        public CompareMethod CompareMethod { get; set; }
        public INumber Value1 { get; set; }
        public INumber Value2 { get; set; }
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

        public CharsToIntegerPart()
            : this(CompareMethod.Any)
        { }

        public CharsToIntegerPart(CompareMethod compareMethod = CompareMethod.Any)
            => CompareMethod = CompareMethod.Any;

        public CharsToIntegerPart(IntegerUnit iu)
        {
            CompareMethod = iu.CompareMethod;
            Value1 = iu.Value1;
            Value2 = iu.Value2;
        }

        public CharsToIntegerPart(long exactValue, CompareMethod compareMethod = CompareMethod.Exact)
            : this(new IntegerUnit(exactValue) as INumber, compareMethod)
        { }
        public CharsToIntegerPart(INumber exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }
        public CharsToIntegerPart(long minValue, long maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
            : this(new IntegerUnit(minValue) as INumber, new IntegerUnit(maxValue) as INumber, compareMethod)
        { }
        public CharsToIntegerPart(INumber minValue, INumber maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }
        public CharsToIntegerPart(long[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            INumber[] array = new INumber[select.Length];
            for (long i = 0; i < select.Length; i++)
                array[i] = new LongConst(select[i]);
            Select = array;
        }
        public CharsToIntegerPart(INumber[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }
        public override int Validate(List<ObjectConst> thing, int startIndex = 0, IVariableLinker vl = null)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is CharConst c))
                    break;
                else if (!char.IsDigit(c) && (c != '-' || i != 0))
                    break;                
                else if (i > 20) // long max length + '-'
                    break;
                sb.Append(c);
            }

            if (!long.TryParse(sb.ToString(), out long l))
                return -1;

            IntegerUnit iu = new IntegerUnit(this);
            return iu.Compare(new LongConst(l), vl) ? startIndex + i : -1;
        }

        public override List<ObjectConst> Random(IVariableLinker vl = null)
        {
            IntegerUnit iu = new IntegerUnit(this);
            return iu.Random(vl).ToString().ToObjectList();
        }
    }
}
