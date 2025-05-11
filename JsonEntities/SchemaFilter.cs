using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace JsonEntities
{
    public class SchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(WeatherForecast))
            {
                schema.Description = "Custom description for WeatherForecast.";
                schema.Properties["TemperatureC"].Description = "Temperature in Celsius.";
                schema.Properties["Summary"].Description = "A brief summary of the weather.";

                schema.Properties.Remove("FieldToIgnore");
                schema.Properties.Add("TemperatureF", new OpenApiSchema
                {
                    Type = "integer",
                    Description = "Temperature in Fahrenheit.",                   
                });
            }
        }
    }
}
