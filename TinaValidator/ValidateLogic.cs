using System.Collections.Generic;
using System;
using System.Text;
using System.Text.Json;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic : Area
    {
        public List<Area> Areas { get; set; } = new List<Area>();
        public Dictionary<string, TNode> TNodes { get; private set; } = new Dictionary<string, TNode>();
        public void Save(TNode node, Area a)
        {
            if (!TNodes.ContainsKey(node.ID))
                TNodes.Add(node.ID, node);

            switch(node)
            {
                case EndNode _:
                    return;
                case AreaStart _:
                    if(!TNodes.ContainsKey(node.NextNode.ID))
                        Save(node.NextNode, a);
                    break;
                case Status st:
                    for (int i = 0; i < st.Choices.Count; i++)
                        Save(st.Choices[i].Node, a);
                    break;
                case null:
                    throw new ArgumentNullException();
                default:
                    if (!TNodes.ContainsKey(node.NextNode.ID))
                        Save(node.NextNode, a);
                    break;
            }
        }

        public string Save(string filePath)
        {
            TNodes.Clear();
            for (int i = 0; i < Areas.Count; i++)            
                Save(Areas[i].InitialStatus, Areas[i]);
            Save(InitialStatus, this);

            ////Node
            //StringBuilder nodeString = new StringBuilder();
            //nodeString.Append("{ \"Nodes\": [");
            //foreach(KeyValuePair<string, TNode> kvp in TNodes)
            //{
            //    nodeString.Append(JsonSerializer.Serialize<object>(kvp.Value));
            //    nodeString.Append(',');
            //}
            //nodeString.Remove(nodeString.Length - 1, 1);
            //nodeString.Append("]");
            //return nodeString.ToString();
            //Area
                
            //}
            JsonSerializerOptions jso = new JsonSerializerOptions
            {
                //ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true
            };
            jso.Converters.Add(new TNodeJsonConverter());            

            string s = JsonSerializer.Serialize<object>(this, jso);
            return s;
            
        }

        public ValidateLogic(Status initialStatus = null)
            : this(null, initialStatus)
        { }

        public ValidateLogic(string name, Status initialStatus = null)
            : base(name, initialStatus, null)
        { }
    }
}
