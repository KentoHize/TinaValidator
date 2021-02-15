using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class BooleanUnit : IUnit
    {
        public bool Value { get; set; }
        public bool Compare(object b)
            => b is bool && ((BooleanUnit)b).Value == Value;

        public object Random()
        {
            Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
            return rnd.Next(2) == 1;
        }
    }
}
