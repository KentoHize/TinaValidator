using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class UnitSet : Sequence
    {
        public string ID { get; set; }
        public List<Unit> Units { get; set; } = new List<Unit>();

        public UnitSet(string id, Unit unit)
        {
            ID = id;
            Units.Add(unit);
        }

        public UnitSet(string id, List<Unit> units = null)
        {
            ID = id;
            if (units != null)
                Units = units;
        }
    }
}
