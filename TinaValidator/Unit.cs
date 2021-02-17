namespace Aritiafel.Artifacts.TinaValidator
{
    public abstract class Unit : IUnit
    {   
        public abstract bool Compare(object b);
        public abstract object Random();
    }
}
