namespace JsonEntities
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        //public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

        public string? FieldToIgnore { get; set; } = "This field will be ignored in serialization.";
    }
}
