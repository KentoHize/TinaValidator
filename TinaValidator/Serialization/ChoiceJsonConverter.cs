using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Locations.StorageHouse;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    
    public class ChoiceJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeToConvert == typeof(Choice);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(                
                typeof(ChoiceJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        private class ChoiceJsonConverterInner<T> : DefaultJsonConverter<T> where T : Choice
        {        
            public ChoiceJsonConverterInner()
            { }

            public override object GetPropertyValueAndWrite(string propertyName, object instance, bool skip = false)
            {
                Type p_type = instance.GetType().GetProperty(propertyName).PropertyType;
                if (typeof(TNode).IsAssignableFrom(p_type))
                    return ((TNode)base.GetPropertyValueAndWrite(propertyName, instance, skip)).ID;
                return base.GetPropertyValueAndWrite(propertyName, instance, skip);
            }
        }
    }
}
