using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class StringToIntegerPart : Part
    {
        public char EndChar { get; set; }

        public override int Validate(List<object> thing, int startIndex)
        {
            throw new NotImplementedException();
        }

        public override List<object> Random()
        {
            throw new NotImplementedException();
        }
    }
}
