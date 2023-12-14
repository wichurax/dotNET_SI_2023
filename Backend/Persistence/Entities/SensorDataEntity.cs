using Backend.Broker;
using MongoDB.Entities;

namespace Backend.Persistence.Entities;

public class SensorDataEntity : Entity
{
	public string SensorType { get; set; } = null!;
	public string SensorName { get; set; } = null!;
	public double Value { get; set; }
	public string Unit { get; set; } = null!;
	public DateTimeOffset MeasurementDate { get; set; }
}