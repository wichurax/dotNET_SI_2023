namespace Backend.Dtos
{
    public class FilterDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

		/// <summary>
		/// Data - that are result of filtering - must contain only sensor types that are provided in this property
		/// </summary>
		public List<string> SensorType { get; set; } = new();

		/// <summary>
		/// Data - that are result of filtering - must contain only sensor names that are provided in this property
		/// </summary>
		public List<string> SensorName { get; set; } = new();
    }
}
