using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class UnitSet : Part
    {
        public List<Unit> Units { get; set; } = new List<Unit>();
        public UnitSet(Unit unit)
            : this(null, unit)
        { }

        public UnitSet(List<Unit> units = null)
            : this(null, units)
        { }

        public UnitSet(string id, Unit unit)
            : base(null, id)
        {
            Units.Add(unit);
        }

        public UnitSet(string id, List<Unit> units = null)
            : base(null, id)
        {
            if (units != null)
                Units = units;
        }

        public override List<object> Random()
        {
            List<object> result = new List<object>();
            for (int i = 0; i < Units.Count; i++)
                result.Add(Units[i].Random());
            return result;
        }

        public override int Validate(List<object> thing, int startIndex)
        {
            for (int i = 0; i < Units.Count; i++)
                if (!Units[i].Compare(thing[startIndex + i]))
                    return -1;
            return startIndex + Units.Count;
        }
    }
}
