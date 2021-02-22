namespace Aritiafel.Artifacts.TinaValidator
{
    public class EndNode : TNode
    {
        public static EndNode Instance { get; } = new EndNode();
        private EndNode(string id = "__ENDNODE62ADB0E6")
            : base(null, id)
        { }

        //public override string Serialize()
        //{   
        //    return $"{{\"ID\": \"{ID}\", \"Type\": \"EndNode\" }}";
        //}
    }
}
