using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class Part : IPart
    {
        //Local Variable ...

        public SetValueBox SetValue { get; set; }
        public Status NextStatus { get; set; } = new Status();

        public virtual bool Compare(List<object> b)
        {
            throw new System.NotImplementedException();
        }

        public virtual List<object> Random()
        {
            throw new System.NotImplementedException();
        }
    }
}