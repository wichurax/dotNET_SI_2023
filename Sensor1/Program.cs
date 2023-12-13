using System.Text.Json;
using Configuration;
using GenericSensorNamespace;
using MQTTnet;
using SensorsFactory;

public class Program
{
	static MessagePublisher publisher = new MessagePublisher();
	
    static async Task Main()
    {
		GenericSensor newSensor = new("temperature");
		Console.WriteLine($"{newSensor.SensorName} has started");

		Double value = Convert.ToDouble(Console.ReadLine());
		String sensorType = newSensor.SensorType;

		var data = new SensorData(sensorType, newSensor.SensorName, value, newSensor.Unit, DateTime.Now);
		var serializedData = JsonSerializer.Serialize(data);
			
		var message = new MqttApplicationMessageBuilder()
			.WithTopic("sensors/" + sensorType)
			.WithPayload(serializedData)
			.Build();
						
		await publisher.Publish(message);
		Console.WriteLine("Done!");
	}
}