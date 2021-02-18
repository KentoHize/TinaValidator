using Aritiafel.Locations;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{

    public abstract class Part : IPart
    {
        //Local Variable ...
        public string ID { get; set; }
        public SetValueBox SetValue { get; set; }
        public Status NextStatus { get; set; }

        public abstract int Validate(List<object> thing, int startIndex = 0);
        public abstract List<object> Random();
        protected Part(Status nextStatus = null, SetValueBox setValueBox = null, string id = null)
        {
            NextStatus = nextStatus;
            SetValue = setValueBox;
            ID = id ?? IdentifyShop.GetNewID("P");
        }
    }
}