// See https://aka.ms/new-console-template for more information
using MQTTnet;
using MQTTnet.Client;

var mqttFactory = new MqttFactory();

using (var mqttClient = mqttFactory.CreateMqttClient())
{
	var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer("localhost", 1883).Build();

	// Setup message handling before connecting so that queued messages
	// are also handled properly. When there is no event handler attached all
	// received messages get lost.
	mqttClient.ApplicationMessageReceivedAsync += e =>
	{
		Console.WriteLine("Received application message.");

		return Task.CompletedTask;
	};

	await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

	var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
		.WithTopicFilter(f => f.WithTopic("sensors/temperature"))
		.Build();

	await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

	Console.WriteLine("MQTT client subscribed to topic.");

	Console.WriteLine("Press enter to exit.");
	Console.ReadLine();
}