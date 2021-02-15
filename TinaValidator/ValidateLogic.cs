using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic
    {
        public string Name { get; set; }
        public Status InitialStatus { get; set; }
        public Status EndStatus { get; set; }
        public List<Part> Choices { get; set; } = new List<Part>();

        public ValidateLogic(Part choice, Status initialStatus = null)
            : this(null, choice, initialStatus)
        { }

        public ValidateLogic(string name, Part choice, Status initialStatus = null)
        {
            Name = name;
            if (choice != null)
                Choices.Add(choice);
            InitialStatus = initialStatus;
            if (InitialStatus == null)
                InitialStatus = new Status();
        }

        public ValidateLogic(string name = null, List<Part> choices = null, Status initialStatus = null)
        {   
            Name = name;
            if (choices != null)
                Choices = choices;
            InitialStatus = initialStatus;
            if (InitialStatus == null)
                InitialStatus = new Status();
        }
    }
}
