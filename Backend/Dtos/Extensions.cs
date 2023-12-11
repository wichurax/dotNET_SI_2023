using Backend.Persistence.Entities;

namespace Backend.Dtos;

public static class Extensions
{
	internal static SensorMeasurementDto ToDto(this SensorDataEntity entity) =>
		new(
			entity.SensorType,
			entity.SensorName,
			entity.Value,
			entity.Unit,
			entity.MeasurementDate);
}