using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class AreaPart : Part
    {
        public Area Area { get; set; }
        public AreaPart(Area area = null, Status nextStatus = null, string id = null)
            : base(nextStatus, null, id)
        {
            Area = area;
        }

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
