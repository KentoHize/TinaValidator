using System.IO;
using System.Runtime.Serialization;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Area
    {
        public string Name { get; set; }
        public Status InitialStatus { get; set; }
        public Area Parent { get; set; }
        public Area(string name = null, Status initialStatus = null, Area parent = null)
        {
            Name = name ?? IdentifyShop.GetNewID("AR");
            InitialStatus = initialStatus;
            Parent = parent;
        }

        //public string Save()
        //{

        //    { using (FileStream fs = new FileStream(filePath, FileMode.Create)
        //    {

        //    }}
        //}
        //public void Load(string filePath)
        //{

        //}
    }
}
