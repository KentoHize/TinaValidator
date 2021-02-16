using System.Collections.Generic;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Part : Area, INode
    {
        //Local Variable ...
        public string ID { get; set; }
        public Area Parent { get; set; }
        public SetValueBox SetValue { get; set; }
        public Status NextStatus { get; set; }
        public abstract bool Compare(List<object> thing);
        public abstract List<object> Random();

        protected Part()
            : this(null)
        { }

        protected Part(string id = null)
        {
            ID = id ?? IdentifyShop.GetNewID("P");
        }
    }
}