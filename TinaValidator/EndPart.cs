using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class EndPart : Part
    {
        public static EndPart Instance { get; } = new EndPart();
        private EndPart()
        { }

        public override List<object> Random()
        {
            throw new NotImplementedException();
        }

        public override int Validate(List<object> thing, int startIndex)
        {
            throw new NotImplementedException();
        }
    }
}
