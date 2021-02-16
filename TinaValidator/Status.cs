﻿using System.Collections.Generic;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status : INode
    {
        public string ID { get; set; }
        public Area Parent { get; set; }
        public List<Part> Choices { get; set; } = new List<Part>();
        public Status(List<Part> choices)
            : this(null, choices)
        { }
        
        public Status(string id = null, List<Part> choices = null)
        {
            ID = id ?? IdentifyShop.GetNewID("ST");
            if (choices != null)
                Choices = choices;
        }

        public Status(Part choice)
            : this(null, choice)
        { }

        public Status(string id, Part
            choice)
        {
            ID = id ?? IdentifyShop.GetNewID("ST");
            if (choice != null)
                Choices.Add(choice);
        }

        public List<object> Random()
        {
            //Normal Random
            throw new System.NotImplementedException();
        }

        public bool Compare(List<object> thing)
        {
            throw new System.NotImplementedException();
        }
    }
}
