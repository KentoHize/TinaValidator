using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic : Area
    {   
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
            : base (name, initialStatus, null)
        {   
            EndStatus = endStatus;
        }        
    }
}
