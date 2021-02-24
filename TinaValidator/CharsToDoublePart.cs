using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToDoublePart : Part
    {
        public CompareMethod CompareMethod { get; set; }
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public CharsToDoublePart()
            => CompareMethod = CompareMethod.Any;

        public CharsToDoublePart(DoubleUnit du)
        {
            CompareMethod = du.CompareMethod;
            Value1 = du.Value1;
            Value2 = du.Value2;
        }

        public CharsToDoublePart(double exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public CharsToDoublePart(double minValue, double maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public override int Validate(List<object> thing, int startIndex = 0)
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
                else if(hasE == 1)
                {
                    if (c != '+' || c != '-')
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
                else if (!char.IsDigit(c) && (c != '-' || i != 0))
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

        public override List<object> Random()
        {
            DoubleUnit du = new DoubleUnit(this);
            return du.Random().ToString().ToObjectList();
        }
    }
}
