using Aritiafel.Locations;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status : TNode
    {
        public Area Parent { get; set; }
        public List<TNode> Choices { get; set; } = new List<TNode>();
        public Status(List<TNode> choices)
            : this(null, choices)
        { }

        public Status(string id = null, List<TNode> choices = null)
            : base(null, id ?? IdentifyShop.GetNewID("ST"))
        {   
            if (choices != null)
                Choices = choices;
        }

        public Status(Part choice)
            : this(null, choice)
        { }

        public Status(string id, TNode choice)
            : base(null, id ?? IdentifyShop.GetNewID("ST"))
        {   
            if (choice != null)
                Choices.Add(choice);
        }
    }
}
