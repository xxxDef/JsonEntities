using JsonEntities;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

public static class JsonConfiguration
{
    public static JsonSerializerOptions ConfigureJson()
    {
        var options = new JsonSerializerOptions
        {
            TypeInfoResolver = new CustomJsonTypeInfoResolver()
        };



        return options;
    }
}

public class CustomJsonTypeInfoResolver : DefaultJsonTypeInfoResolver
{
    public override JsonTypeInfo GetTypeInfo(Type type, JsonSerializerOptions options)
    {
        var typeInfo = ViewTypeInfo.GetViewTypeInfo(type);
        if (typeInfo != null)
        {
            var jsonTypeInfo = JsonTypeInfo.CreateJsonTypeInfo(type, options);

            foreach (var pi in typeInfo.Properties)
            {
                var property = jsonTypeInfo.CreateJsonPropertyInfo(pi.Type, pi.Name);
                property.Set = (obj, val) =>
                {
                    var value = obj.GetType().GetProperty("Value").GetValue(obj, null);

                    if (pi.Set != null)
                    {
                        pi.Set(value, val);
                    }
                    else
                        value.GetType().GetProperty(pi.Name).SetValue(value, val, null);
                };
                property.Get = (obj) =>
                {
                    var value = obj.GetType().GetProperty("Value").GetValue(obj, null);
                    if (pi.Get != null)
                        return pi.Get(value);
                    else

                        return value.GetType().GetProperty(pi.Name)?.GetValue(value, null);
                };

                jsonTypeInfo.Properties.Add(property);
            }
            return jsonTypeInfo;
        }

        return base.GetTypeInfo(type, options);
    }
}
