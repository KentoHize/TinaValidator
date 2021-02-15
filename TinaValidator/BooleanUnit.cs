using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class BooleanUnit : Unit, IUnit
    {
        public bool Value { get; set; }
        public BooleanUnit(bool value)
            => Value = value;

        public bool Compare(object b)
            => b is bool && ((BooleanUnit)b).Value == Value;
        public object Random()
        {
            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
            return rnd.Next(2) == 1;
        }
    }
}
