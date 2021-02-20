using Aritiafel.Locations;
using Aritiafel.Artifacts.Calculator;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status : TNode
    {
        public Area Parent { get; set; }
        public List<Choice> Choices { get; set; } = new List<Choice>();
        public Status(List<Choice> choices)
            : this(null, choices)
        { }

        public Status(string id = null, List<Choice> choices = null)
            : base(null, id ?? IdentifyShop.GetNewID("ST"))
        {   
            if (choices != null)
                Choices = choices;
        }

        public Status(Choice choice)
            : this(null, choice)
        { }

        public Status(string id, Choice choice)
            : base(null, id ?? IdentifyShop.GetNewID("ST"))
        {   
            if (choice != null)
                Choices.Add(choice);
        }
    }
}
