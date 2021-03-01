using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToDoublePart : Part
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
                        if (value[i] == double.NaN)
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private double[] _Select;

        public CharsToDoublePart()
            : this(CompareMethod.Any)
        { }
        public CharsToDoublePart(DoubleUnit du)
        {
            CompareMethod = du.CompareMethod;
            Value1 = du.Value1;
            Value2 = du.Value2;
        }
        public CharsToDoublePart(CompareMethod compareMethod = CompareMethod.Any)
            => CompareMethod = compareMethod;

        public CharsToDoublePart(double exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }

        public CharsToDoublePart(double minValue, double maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public CharsToDoublePart(double[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            Select = select;
        }

        public override int Validate(List<ObjectConst> thing, int startIndex = 0)
        {
            StringBuilder sb = new StringBuilder();
            bool hasPoint = false;
            int hasE = 0;
            int i;
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char c))
                    break;
                else if (c == '.')
                {
                    if (hasPoint)
                        break;
                    else
                        hasPoint = true;
                }
                else if (hasE == 1)
                {
                    if (c != '+' && c != '-')
                    {
                        sb.Remove(sb.Length - 1, 1);
                        break;
                    }
                    hasE++;
                }
                else if (c == 'E' || c == 'e')
                {
                    if (hasE == 0)
                        hasE++;
                    else
                        break;
                }
                else if (!char.IsDigit(c) && (i != 0 || c != '-'))
                    break;
                else if (i > 329) // too long for a double
                    break;
                sb.Append(c);
            }

            if (!double.TryParse(sb.ToString(), out double d))
                return -1;

            DoubleUnit du = new DoubleUnit(this);
            return du.Compare(d) ? startIndex + i : -1;
        }

        public override List<ObjectConst> Random()
        {
            DoubleUnit du = new DoubleUnit(this);
            return du.Random().ToString().ToObjectList();
        }
    }
}
