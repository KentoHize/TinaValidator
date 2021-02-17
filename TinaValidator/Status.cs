﻿using System.Collections.Generic;
using Aritiafel.Locations;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Status
    {
        public static Status EndStatus { get; } = new Status("__ENDSTATUS");
        public static Status PartEndStatus { get; } = new Status("__PARTENDSTATUS");
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
    }
}
