using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic : Area
    {
        public List<Area> Areas { get; set; } = new List<Area>();
        public void Save(string filePath)
        {

        }

        public void Load(string filePath)
        {

        }

        public ValidateLogic(Status initialStatus = null)
            : this(null, initialStatus)
        { }

        public ValidateLogic(string name, Status initialStatus = null)
            : base(name, initialStatus, null)
        { }
    }
}
