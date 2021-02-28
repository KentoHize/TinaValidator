using System.IO;
using System.Runtime.Serialization;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Area
    {
        public string Name { get; set; }
        public TNode StartNode { get; set; }        
        public Area Parent { get; set; }
        public Area()
            : this(null)
        { }

        public Area(string name = null, TNode startNode = null, Area parent = null)
        {
            Name = name ?? IdentifyShop.GetNewID("AR");
            StartNode = startNode;
            Parent = parent;
        }
    }
}
