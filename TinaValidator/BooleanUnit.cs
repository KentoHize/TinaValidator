using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class BooleanUnit : Unit, IUnit
    {
        public CompareMethod CompareMethod { get; set; }
        public bool Value { get; set; }

        public BooleanUnit()
            => CompareMethod = CompareMethod.Any;
        public BooleanUnit(bool value)
        {
            CompareMethod = CompareMethod.Exact;
            Value = value;
        }   

        public bool Compare(object b)
        {
            if (!(b is bool))
                return false;
            if (CompareMethod == CompareMethod.Any)
                return true;
            else
                return ((BooleanUnit)b).Value == Value;
        }
        
        public object Random()
        {
            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
            return rnd.Next(2) == 1;
        }
    }
}
