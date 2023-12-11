namespace Backend.Dtos
{
    public class FilterDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<string> SensorType { get; set; } = new();
        public List<string> SensorName { get; set; } = new();
    }
}
