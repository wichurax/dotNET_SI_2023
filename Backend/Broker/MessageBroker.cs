using MQTTnet.Client;
using MQTTnet;
using Newtonsoft.Json;
using Backend.Persistence;

namespace Backend.Broker;

internal class MessageBroker
{
	private readonly MongoDbService _mongoDbService;

	public MessageBroker(MongoDbService mongoDbService)
	{
		_mongoDbService = mongoDbService;
	}

	public async Task SubscribeToTopics(List<string> topics)
	{
		var mqttFactory = new MqttFactory();
		using var mqttClient = mqttFactory.CreateMqttClient();
		var mqttClientOptions = new MqttClientOptionsBuilder()
			//.WithTcpServer("localhost", 1883)
			.WithTcpServer("host.docker.internal", 1883)
			.Build();

		// Setup message handling before connecting so that queued messages
		// are also handled properly. When there is no event handler attached all
		// received messages get lost.
		mqttClient.ApplicationMessageReceivedAsync += e =>
		{
			var payload = e.ApplicationMessage.ConvertPayloadToString();
			try
			{
				var sensorData = JsonConvert.DeserializeObject<SensorData>(payload) ?? throw new Exception("object shoudn't be null");
				var sensorDataEntity = sensorData.ToEntity();

				if (sensorData != null) _mongoDbService.InsertSensorData(sensorDataEntity);
				Console.WriteLine($"Inserted data into MongoDB: {JsonConvert.SerializeObject(sensorDataEntity)}");
			}
			catch (JsonException ex)
			{
				Console.WriteLine($"Failed to parse JSON: {ex.Message}");
			}

			return Task.CompletedTask;
		};

		await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

		var mqttSubscribeOptions = mqttFactory
			.CreateSubscribeOptionsBuilder()
			.FilterToTopics(topics)
			.Build();

		await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

		topics
			.Select(topic => $"Subscribed to topic: {topic}")
			.ToList()
			.ForEach(log => Console.WriteLine(log));

		Console.ReadLine();
	}
}

internal static class MessageBrokerExtensions
{
	public static MqttClientSubscribeOptionsBuilder FilterToTopics(
		this MqttClientSubscribeOptionsBuilder subscriptionBuilder, 
		List<string> topics)
	{
		foreach (var topic in topics)
		{
			subscriptionBuilder.WithTopicFilter(f => f.WithTopic(topic));
		}

		return subscriptionBuilder;
	}
}
