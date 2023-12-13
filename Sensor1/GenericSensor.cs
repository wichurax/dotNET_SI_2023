using Configuration;

namespace GenericSensorNamespace
{
	internal class GenericSensor
	{
		public string SensorType { get; set; }
		public string SensorName { get; set; }
		public Double MinValue { get; set; }
		public Double MaxValue { get; set; }
		public Double Interval { get; set; }
		public string Unit { get; set; }

		public static int id;
		public static Random rand = new();
	
		static SensorsConfiguration _configuration = null!;

		public GenericSensor(string sensorType)
    	{
			_configuration = SensorsConfigurationService.Get();
			SensorType = sensorType;
			SensorName = sensorType[0].ToString().ToUpper() + id++;
			Unit = _configuration.SensorsList.Where(sensor => sensor.Type == sensorType).Select(sensor => sensor.Unit).FirstOrDefault();
			Unit ??= "C";
			MinValue = _configuration.SensorsList.Where(sensor => sensor.Type == sensorType).Select(sensor => sensor.MinValue).FirstOrDefault();
			MaxValue = _configuration.SensorsList.Where(sensor => sensor.Type == sensorType).Select(sensor => sensor.MaxValue).FirstOrDefault();
			Interval = _configuration.SensorsList.Where(sensor => sensor.Type == sensorType).Select(sensor => sensor.Interval).FirstOrDefault();
    	}


		public double GenereteNextValue()
		{
			var value = MinValue + (rand.NextDouble() * (MaxValue - MinValue));
			return value;
		}
	}
}
