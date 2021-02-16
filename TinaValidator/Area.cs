using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Area 
    {
        public string Name { get; set; }
        public Status InitialStatus { get; set; }
        public Area Parent { get; set; }       
        public Area(string name = null, Status initialStatus = null, Area parent = null)
        {
            Name = name;
            InitialStatus = initialStatus;
            Parent = parent;
        }

        //public string ID { get; set; }
        //public UnitSet UnitSet { get; set; }
        //public bool Compare(List<object> thing)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<object> Random()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
