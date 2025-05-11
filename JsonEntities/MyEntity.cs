namespace JsonEntities
{
    public class MyEntity
    {
        public required Guid DbId { get; set; }
        public string? Name { get; set; }
        public string? DbVersion { get; set; }
    }
}
