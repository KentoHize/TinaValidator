using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public const int Invalid = -1;
        public ValidateLogic Logic { get; set; }
        public long LongerErrorLocation { get; set; }
        public string Message { get; set; }
        public TinaValidator(ValidateLogic logic = null)
        {
            Logic = logic;
        }

        public bool Validate(List<object> things)
        {
            if (things == null)
                throw new ArgumentNullException(nameof(things));
            LongerErrorLocation = 0;  
            TVData tv = new TVData(0, Logic.InitialStatus);
            return BFS_NodeValidate(things, tv) != Invalid;
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        private int BFS_NodeValidate(List<object> things, TVData data)
        {
            Queue<TVData> nodeQueue = new Queue<TVData>();
            HashSet<TVData> invisitedRecords = new HashSet<TVData>();
            nodeQueue.Enqueue(data);
            while (nodeQueue.Count != 0)
            {
                if (data.Index > LongerErrorLocation)
                    LongerErrorLocation = data.Index;
                int nextIndex;
                TVData newData;
                data = nodeQueue.Dequeue();                
                switch (data.Node)
                {
                    case Part p:
                        nextIndex = p.Validate(things, data.Index);
                        if (nextIndex == Invalid)
                            continue;
                        newData = new TVData(nextIndex, p.NextNode, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case Status st:
                        if (!invisitedRecords.Add(data))
                            continue;
                        for (int i = 0; i < st.Choices.Count; i++)
                        {
                            if (st.Choices[i].Conditon == null || Calculator.Calculator.CalculateBooleanExpression(st.Choices[i].Conditon, data.Memory))
                            {
                                if (i == 0)
                                    newData = new TVData(data.Index, st.Choices[i].Node, data.AreaNextNode, data.Memory);
                                else
                                {
                                    Stack<TNode> newStack = new Stack<TNode>(data.AreaNextNode.Reverse());                                    
                                    newData = new TVData(data.Index, st.Choices[i].Node, newStack, new TVMemory(data.Memory));
                                }
                                nodeQueue.Enqueue(newData);
                            }
                        }
                        break;
                    case EndNode _:
                        if (data.AreaNextNode.Count == 0)
                            if (data.Index == things.Count)
                                return data.Index;
                            else
                                continue;
                        TNode tn = data.AreaNextNode.Pop();
                        newData = new TVData(data.Index, tn, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case AreaStart ars:
                        data.AreaNextNode.Push(ars.NextNode);
                        newData = new TVData(data.Index, ars.Area.InitialStatus, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    case Execute ex:
                        Calculator.Calculator.RunStatements(ex.Statements, data.Memory);
                        newData = new TVData(data.Index, ex.NextNode, data.AreaNextNode, data.Memory);
                        nodeQueue.Enqueue(newData);
                        break;
                    default:
                        throw new Exception("!?");
                }
            }           
            return Invalid;
        }
        public string CreateRandomToString()
        {
            StringBuilder sb = new StringBuilder();
            List<object> randomList = CreateRandom();
            for (int i = 0; i < randomList.Count; i++)
                sb.Append(randomList[i]);
            return sb.ToString();
        }

        public List<object> CreateRandom()
        {
            List<object> result = new List<object>();
            TVData tv = new TVData(Logic.InitialStatus);
            NodeCreateRandom(result, tv);
            return result;
        }

        private void NodeCreateRandom(List<object> result, TVData data)
        {   
            switch (data.Node)
            {
                case EndNode _:
                    return;
                case AreaStart ars:
                    NodeCreateRandom(result, new TVData(ars.Area.InitialStatus, data.Memory));
                    NodeCreateRandom(result, new TVData(ars.NextNode, data.Memory));
                    break;
                case Execute e:
                    Calculator.Calculator.RunStatements(e.RunRandomStatement ? e.RandomStatements : e.Statements, data.Memory);
                    NodeCreateRandom(result, new TVData(data.Node.NextNode, data.Memory));
                    break;
                case Part p:
                    result.AddRange(p.Random());
                    NodeCreateRandom(result, new TVData(data.Node.NextNode, data.Memory));
                    break;
                case Status st:
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    SortedList<int, TNode> ratioThreshold = new SortedList<int, TNode>();
                    int RationCount = 0;
                    for (int i = 0; i < st.Choices.Count; i++)
                    {
                        if (st.Choices[i].Conditon == null || Calculator.Calculator.CalculateBooleanExpression(st.Choices[i].Conditon, data.Memory))
                        {
                            RationCount += st.Choices[i].RadomRatio;
                            ratioThreshold.Add(RationCount, st.Choices[i].Node);
                        }
                    }
                    if (ratioThreshold.Count == 0)
                        throw new Exception($"No Route in {st.ID}");
                    int index = rnd.Next(RationCount);
                    if (ratioThreshold.ContainsKey(index))                    
                        NodeCreateRandom(result, new TVData(ratioThreshold.Values[ratioThreshold.IndexOfKey(index) + 1], data.Memory));                     
                    else
                    {
                        ratioThreshold.Add(index, null);
                        NodeCreateRandom(result, new TVData(ratioThreshold.Values[ratioThreshold.IndexOfValue(null) + 1], data.Memory));                        
                    }
                    break;
            }
        }
    }
}
