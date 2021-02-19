using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class CalculatorMemory : IVariableLinker
    {
        Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
        Dictionary<string, Type> VariablesType { get; set; } = new Dictionary<string, Type>();
        private List<object> CreateArray(byte dimensions, List<int> counts, byte dimensionIndex = 0)
        {
            List<object> result = new List<object>();            
            if (dimensions <= 0)
                return result;
            for (int i = 0; i < counts[dimensionIndex]; i++)
                result.Add(CreateArray(--dimensions, counts, ++dimensionIndex));
            return result;
        }            

        public void DeclareVariable(string name, Type type, byte dimensions = 0, List<int> counts = null, IObject value = null)
        {
            if (dimensions > 10 || dimensions < 0)
                throw new ArgumentOutOfRangeException("dimensions");
            if(Variables.ContainsKey(name))
                throw new Exception();

            if (dimensions == 0)
            {
                Variables.Add(name, value.GetObject(this));
                VariablesType.Add(name, type);
            }                
            else
            {
                List<object> array = CreateArray(dimensions, counts);
                Variables.Add(name, array);
                VariablesType.Add(name, type);
            }
        }
        public object GetValue(Variable v)
        {
            if (!Variables.ContainsKey(v.Name))
                throw new ArgumentNullException();
            if (v.Keys == null)
                return Variables[v.Name];
            else
            {
                object o = Variables[v.Name];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = (o as List<object>)[(int)v.Keys[i]];
                return (o as List<object>)[(int)v.Keys[v.Keys.Count - 1]];
            }
        }

        public void SetValue(Variable v, object value)
        {
            if (Variables.ContainsKey(v.Name))
                throw new Exception();
            if (v.Keys == null)
                Variables[v.Name] = value;
            else
            {
                object o = Variables[v.Name];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = (o as List<object>)[(int)v.Keys[i]];
                (o as List<object>)[(int)v.Keys[v.Keys.Count - 1]] = value;
            }   
        }
    }
}
