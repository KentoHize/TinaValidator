using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class SkipPart : Part
    {
        public override List<object> Random()
        {
            return new List<object>();
        }

        public override int Validate(List<object> thing, int startIndex)
        {
            return startIndex;
        }
    }
}
