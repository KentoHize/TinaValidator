using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic
    {
        public string Name { get; set; }
        public Status InitialStatus { get; set; }
        public Status EndStatus { get; set; }
        public void Save(string filePath)
        {
            
        }

        public void Load(string filePath)
        {

        }

        public ValidateLogic(Status initialStatus = null, Status endStatus = null)
            : this(null, initialStatus, endStatus)
        { }

        public ValidateLogic(string name, Status initialStatus = null, Status endStatus = null)
        {
            Name = name;
            InitialStatus = initialStatus;
            if (InitialStatus == null)
                InitialStatus = new Status();
            EndStatus = endStatus;
        }
    }
}
