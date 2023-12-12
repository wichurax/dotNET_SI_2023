using Backend.Broker;
using MongoDB.Entities;

namespace Backend.Persistence.Entities;

public class SensorDataEntity : Entity
{
	public string SensorType { get; set; }
	public string SensorName { get; set; }
	public double Value { get; set; }
	public string Unit { get; set; }
	public DateTimeOffset MeasurementDate { get; set; }
}