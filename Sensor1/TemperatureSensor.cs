namespace Sensor1
{
	internal class TemperatureSensor
	{
		public static double GenereteNextValue()
		{
			var rand = new Random();

			var value = rand.NextDouble()*25;

			return value;
		}
    }
}
