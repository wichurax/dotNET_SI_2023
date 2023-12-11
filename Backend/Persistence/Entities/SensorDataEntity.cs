using MongoDB.Entities;

namespace Backend.Persistence.Entities;

internal class SensorDataEntity : Entity
{
	public string SensorType { get; set; }
	public string SensorName { get; set; }
	public double Value { get; set; }
	public string Unit { get; set; }
	public DateTime MeasurementDate { get; set; }
}