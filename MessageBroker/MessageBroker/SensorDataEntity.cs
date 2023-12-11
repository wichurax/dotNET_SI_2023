using MongoDB.Entities;

namespace MessageBroker;

internal class SensorDataEntity : Entity
{
	public string SensorType { get; set; }
	public string SensorName { get; set; }
	public double Value { get; set; }
	public string Unit { get; set; }
	public DateTime MeasurementDate { get; set; }
}

internal static class Extensions
{
	public static SensorDataEntity ToEntity(this SensorData data) =>
		new()
		{
			ID = Guid.NewGuid().ToString(),
			SensorType = data.SensorType,
			SensorName = data.SensorName,
			Value = data.Value,
			Unit = data.Unit,
			MeasurementDate = data.MeasurementDate
		};
}
