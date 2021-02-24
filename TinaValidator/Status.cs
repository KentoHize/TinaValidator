using Aritiafel.Locations;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status : TNode
    {
        public Area Parent { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
        public Status(List<Choice> choices, Area parent = null)
            : this(null, parent, choices)
        { }

        public Status(string id = null, Area parent = null, List < Choice> choices = null)
            : base(null, parent, id ?? IdentifyShop.GetNewID("ST"))
        {
            if (choices != null)
                Choices = choices;
        }

        public Status(Choice choice, Area parent = null)
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
