using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Part : IPart
    {
        //Local Variable ...

        public SetValueBox SetValue { get; set; }
        public Status NextStatus { get; set; } = new Status();

        public abstract bool Compare(List<object> thing);
        public abstract List<object> Random();
    }
}