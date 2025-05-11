using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

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

    public class CustomParameterFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var viewTypeInfo = ViewTypeInfo.GetViewTypeInfo(context.Type);
            if (viewTypeInfo == null)
                return;


            schema.Description += $" ViewTypeInfo: {viewTypeInfo}";

            schema.Properties = viewTypeInfo.Properties.ToDictionary(
                p => p.Name,
                p => new OpenApiSchema
                {
                    Type = p.Type.Name,
                    Description = p.Description,
                    Nullable = p.IsNullable,
                    Format = p.Format
                });
        }
    }
}

