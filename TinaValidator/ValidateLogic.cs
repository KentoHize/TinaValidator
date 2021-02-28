using System.Collections.Generic;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Aritiafel.Artifacts.TinaValidator.Serialization;
using Aritiafel.Artifacts.Calculator;
using Aritiafel.Locations.StorageHouse;
using System.IO;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class ValidateLogic : Area
    {
        public List<Area> Areas { get; set; } = new List<Area>();
        public Dictionary<string, TNode> TNodes { get; set; } = new Dictionary<string, TNode>();
        //public Dictionary<string, Variable> Variables { get; set; } = new Dictionary<string, Variable>();
        private void Save(TNode node, Area a)
        {
            if (node == null)
                throw new Exception($"Node Missing");

            if (TNodes.ContainsKey(node.ID))
                return;
            TNodes.Add(node.ID, node);

            switch(node)
            {
                case EndNode _:
                    return;
                case AreaStart _:
                    Save(node.NextNode, a);
                    break;
                case Status st:
                    for (int i = 0; i < st.Choices.Count; i++)
                        Save(st.Choices[i].Node, a);
                    break;
                case null:
                    throw new ArgumentNullException(nameof(node));
                default:
                    Save(node.NextNode, a);
                    break;
            }
        }

        public void Load(string filePath)
        {   
            string jsonString;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    jsonString = sr.ReadToEnd();
                }
            }

            JsonSerializerOptions jso = new JsonSerializerOptions
            { WriteIndented = true };
            jso.Converters.Add(new TNodeJsonConverter());
            jso.Converters.Add(new AreaJsonConverter());
            jso.Converters.Add(new ChoiceJsonConverter());
            jso.Converters.Add(new StatementJsonConverter());
            jso.Converters.Add(new UnitConverter());
            jso.Converters.Add(new OtherJsonConverter());
            ValidateLogic vl = JsonSerializer.Deserialize<ValidateLogic>(jsonString, jso);
            TNodes = vl.TNodes;
            Areas = vl.Areas;
            Name = vl.Name;            
            StartNode = TNodes[vl.StartNode.ID];
            for(int i = 0; i < Areas.Count; i++)
            {
                if (Areas[i].StartNode != null)
                    Areas[i].StartNode = TNodes[(Areas[i].StartNode as IDNode).ID];
                if (Areas[i].Parent != null)
                    if (Name == (Areas[i].Parent as IDArea).Name)
                        Areas[i].Parent = this;
                    else
                        Areas[i].Parent = Areas.Find(m => m.Name == (Areas[i].Parent as IDArea).Name);
            }
                
            foreach(KeyValuePair<string, TNode> kv in TNodes)
            {   
                if (kv.Value.NextNode != null)
                    kv.Value.NextNode = TNodes[(kv.Value.NextNode as IDNode).ID];
                if (kv.Value.Parent != null)
                    if(Name == (kv.Value.Parent as IDArea).Name)
                        kv.Value.Parent = this;
                    else
                        kv.Value.Parent = Areas.Find(m => m.Name == (kv.Value.Parent as IDArea).Name);
                if (kv.Value is Status st)
                    for (int i = 0; i < st.Choices.Count; i++)                        
                        st.Choices[i].Node = st.Choices[i].Node != null ?
                            TNodes[(st.Choices[i].Node as IDNode).ID] : null;
            }
        }

        public void Save(string filePath)
        {
            TNodes.Clear();
            for (int i = 0; i < Areas.Count; i++)            
                Save(Areas[i].StartNode, Areas[i]);
            Save(StartNode, this);
            JsonSerializerOptions jso = new JsonSerializerOptions
            { WriteIndented = true };

            jso.Converters.Add(new TNodeJsonConverter());
            jso.Converters.Add(new AreaJsonConverter());
            jso.Converters.Add(new ChoiceJsonConverter());
            jso.Converters.Add(new StatementJsonConverter());
            jso.Converters.Add(new UnitConverter());
            jso.Converters.Add(new OtherJsonConverter());
            string s = JsonSerializer.Serialize(this, jso);
            using(FileStream fs = new FileStream(filePath, FileMode.Create))                
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(s);
                }
            }
        }

        public ValidateLogic()
            : this(null)
        { }
        public ValidateLogic(TNode startNode = null)
            : this(null, startNode)
        { }

        public ValidateLogic(string name, TNode startNode = null)
            : base(name, startNode, null)
        { }
    }
}
