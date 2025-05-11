using System.Reflection;

namespace JsonEntities
{
    public class IViewInfo<T> where T: new()
    {
        public required T Value { get; set; } = new T();
    }

    public record ViewPropertyInfo(
        string Name,
        Type Type,
        string Description,
        bool IsRequired,
        bool IsReadOnly,
        bool IsWriteOnly,
        bool IsNullable,
        string? Format,
        Action<Object, Object>? Set = null,
        Func<Object, Object>? Get = null);


    public record ViewTypeInfo(string Name, IEnumerable<ViewPropertyInfo> Properties)
    {
        public static ViewTypeInfo? GetViewTypeInfo(Type type)
        {
            var prop = type.GetProperty("ViewTypeInfo", BindingFlags.Static | BindingFlags.Public);
            if (prop == null)
                return null;
            return (ViewTypeInfo?)prop.GetValue(null);
        }
    }

    public class WeatherForecastView : IViewInfo<WeatherForecast>
    {
        public static ViewTypeInfo ViewTypeInfo => new ViewTypeInfo(nameof(WeatherForecast),
            new List<ViewPropertyInfo>
            {
                new ViewPropertyInfo("Date", typeof(DateOnly), "The date of the weather forecast.", true, false, false, false, "date"),
                new ViewPropertyInfo("TemperatureC", typeof(int), "Temperature in Celsius.", true, false, false, false, "integer"),
                new ViewPropertyInfo("Summary", typeof(string), "A brief summary of the weather.", true, false, false, true, null),
                new ViewPropertyInfo("TemperatureF", typeof(int), "This field will be ignored in serialization.", false, false, false, true, null,
                    (obj, val) => ((WeatherForecast)obj).TemperatureC = (int)val-35,
                    (obj) => ((WeatherForecast)obj).TemperatureC + 35),
            });
    }
}

