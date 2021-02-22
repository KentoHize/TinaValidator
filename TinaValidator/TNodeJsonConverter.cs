using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TNodeJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if(typeToConvert.BaseType == typeof(TNode))
                return true;
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(TNodeJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { options }, null); ;
        }

        private class TNodeJsonConverterInner<T> : JsonConverter<T> where T : TNode
        {
            public TNodeJsonConverterInner(JsonSerializerOptions options)
            { }

            //private TNodeJsonConverterInner<T>()
            //{ }
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                writer.WriteStartObject();
                PropertyInfo[] pis = typeof(T).GetProperties();
                writer.WriteString("Type", typeof(T).Name);
                //PropertyInfo piID = typeof(T).GetProperty("ID");
                //writer.WriteString("ID", piID.GetValue(value).ToString());
                foreach(PropertyInfo pi in pis)
                {
                    if (pi.Name == "ID")
                        continue;
                    else if(pi.PropertyType == typeof(TNode) && pi.GetValue(value) != null)
                    {
                        writer.WriteStartObject();
                        writer.WriteString(pi.Name, (pi.GetValue(value) as TNode).ID);
                        writer.WriteEndObject();
                        Console.WriteLine(pi.Name);
                        //writer.WriteString(pi.Name, ((TNode)pi.GetValue(value)).ID);
                    }                   
                    else
                    {
                        //Console.WriteLine(pi.GetValue(value)?.ToString());
                        JsonConverter jc = options.GetConverter(pi.PropertyType);
                        writer.WritePropertyName(pi.Name);
                        jc.GetType().GetMethod("Write").Invoke(jc, new object[] { writer, pi.GetValue(value), options });
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
