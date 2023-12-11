namespace Backend.Dtos
{
	public class SensorMeasurementDto
	{
		public string SensorType { get; }
		public string SensorName { get; }
		public double Value { get; }
		public string Unit { get; }
		public DateTimeOffset MeasurementDate { get; }

		public SensorMeasurementDto(string sensorType, string sensorName, double value, string unit, DateTimeOffset measurementDate)
		{
			SensorType = sensorType;
			SensorName = sensorName;
			Value = value;
			Unit = unit;
			MeasurementDate = measurementDate;
		}
	}
}
