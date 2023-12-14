using Backend.Persistence.Entities;

namespace Backend.Broker;

internal class SensorData
{
	public string SensorType { get; set; } = null!;
	public string SensorName { get; set; } = null!;
	public double Value { get; set; }
	public string Unit { get; set; } = null!;
	public DateTime MeasurementDate { get; set; }
}

internal static class SensorDataExtensions
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