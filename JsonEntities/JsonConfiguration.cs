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
            //PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    (JsonTypeInfo typeInfo) =>
                    {
                        if (typeInfo.Type == typeof(WeatherForecast))
                        {
                            foreach (var property in typeInfo.Properties)
                            {
                                if (property.Name == "TemperatureC")
                                {
                                    property.Name = "temperature_cel";
                                }
                            }
                            var propertyToIgnore = typeInfo.Properties.FirstOrDefault(p => p.Name == "fieldToIgnore");
                            if (propertyToIgnore != null)
                            {
                                typeInfo.Properties.Remove(propertyToIgnore);
                            }

                            var farProp = typeInfo.CreateJsonPropertyInfo(typeof(int), "temperature_far");
                            farProp.Get = (weatherForecast) => 32 + ((WeatherForecast)weatherForecast).TemperatureC;
                            farProp.Set = (weatherForecast, value) => ((WeatherForecast)weatherForecast).TemperatureC = value is int v ? v - 32 : 0;

                            typeInfo.Properties.Add(farProp);
                        }
                    }
                }
            }
        };

        return options;
    }
}
