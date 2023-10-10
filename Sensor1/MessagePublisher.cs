using MQTTnet.Client;
using MQTTnet;

namespace SensorsFactory
{
	internal class MessagePublisher
	{
		public async Task Publish(MqttApplicationMessage message)
		{

			try
			{
				var actory = new MqttFactory();

				using (var client = actory.CreateMqttClient())
				{
					var options = new MqttClientOptionsBuilder().WithTcpServer("localhost", 1883).Build();

					await client.ConnectAsync(options, CancellationToken.None);
					await client.PublishAsync(message, CancellationToken.None);
					await client.DisconnectAsync();

					Console.WriteLine("MQTT application message is published.");
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
