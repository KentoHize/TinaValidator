using Aritiafel.Locations;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status : TNode
    {
        public Area Parent { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
        public Status(Area parent, List<Choice> choices)
            : this(null, parent, choices)
        { }

        public Status(string id = null, Area parent = null, List < Choice> choices = null)
            : base(null, parent, id ?? IdentifyShop.GetNewID("ST"))
        {
            if (choices != null)
                Choices = choices;
        }

        public Status(Area parent, Choice choice = null)
            : this(null, parent, choice)
        { }

        public Status(string id, Area parent, Choice choice)
            : base(null, parent, id ?? IdentifyShop.GetNewID("ST"))
        {
            if (choice != null)
                Choices.Add(choice);
        }
    }
}
