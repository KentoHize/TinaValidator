using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Locations.StorageHouse;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class TNodeJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(TNode).IsAssignableFrom(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(TNodeJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        private class TNodeJsonConverterInner<T> : DefaultJsonConverter<T> where T : TNode
        {        
            public TNodeJsonConverterInner()
            { }

            public override void SetPropertyValue(string propertyName, object instance, object value)
            {
                Type p_type = instance.GetType().GetProperty(propertyName).PropertyType;                
                if (CanConvert(p_type))
                    value = value != null ? new IDNode(value.ToString()) : null;
                else if (typeof(Area).IsAssignableFrom(p_type))
                    value = value != null ? new IDArea(value?.ToString()) : null;
                base.SetPropertyValue(propertyName, instance, value);
            }

            public override object GetPropertyValueAndWrite(string propertyName, object instance, bool skip = false)
            {
                Type p_type = instance.GetType().GetProperty(propertyName).PropertyType;
                //if (propertyName == "ID")
                //    skip = true;
                if(propertyName == "ExcludeChars")
                    return base.GetPropertyValueAndWrite(propertyName, instance, skip);
                if (CanConvert(p_type))
                    return ((TNode)base.GetPropertyValueAndWrite(propertyName, instance, skip))?.ID;
                else if (typeof(Area).IsAssignableFrom(p_type))
                    return ((Area)base.GetPropertyValueAndWrite(propertyName, instance, skip))?.Name;
                return base.GetPropertyValueAndWrite(propertyName, instance, skip);
            }
        }
    }
}
