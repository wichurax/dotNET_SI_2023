// See https://aka.ms/new-console-template for more information

using MQTTnet;
using Sensor1;
using SensorsFactory;

var publisher = new MessagePublisher();

for (int i = 0; i < 100; i++)
{
	var temperatureValue = TemperatureSensor.GenereteNextValue();

	Console.WriteLine(temperatureValue);

	var message = new MqttApplicationMessageBuilder()
		.WithTopic("sensors/temperature")
		.WithPayload(temperatureValue.ToString())
		.Build();

	await publisher.Publish(message);
}
