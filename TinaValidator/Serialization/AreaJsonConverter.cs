using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Locations.StorageHouse;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class AreaJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(Area).IsAssignableFrom(typeToConvert);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(AreaJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        private class AreaJsonConverterInner<T> : DefaultJsonConverter<T> where T : Area
        {        
            public AreaJsonConverterInner()
            { }

            public override void SetPropertyValue(string propertyName, object instance, object value)
            {
                if (propertyName == "StartNode")
                    value = value != null ? new IDNode(value.ToString()) : null;
                else if (CanConvert(instance.GetType().GetProperty(propertyName).PropertyType))
                    value = value != null ? new IDArea(value?.ToString()) : null;
                base.SetPropertyValue(propertyName, instance, value);
            }

            public override object GetPropertyValueAndWrite(string propertyName, object instance, bool skip = false)
            {
                if (propertyName == "StartNode")
                    return ((TNode)base.GetPropertyValueAndWrite(propertyName, instance, skip))?.ID;
                if(CanConvert(instance.GetType().GetProperty(propertyName).PropertyType))
                    return ((Area)base.GetPropertyValueAndWrite(propertyName, instance, skip))?.Name;
                return base.GetPropertyValueAndWrite(propertyName, instance, skip);
            }
          
        }
    }
}
