﻿namespace Backend.Dtos
{
	public class SensorMeasurementDto
	{
		public string SensorType { get; set; }
		public string SensorName { get; set; }
		public double Value { get; set; }
		public string Unit { get; set; }
		public DateTime MeasurementDate { get; set; }
	}
}
