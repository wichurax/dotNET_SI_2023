﻿using MQTTnet.Client;
using MQTTnet;

namespace SensorsFactory
{
	internal class MessagePublisher
	{
		public async Task Publish(MqttApplicationMessage message)
		{

			try
			{
				var factory = new MqttFactory();

				using (var client = factory.CreateMqttClient())
				{
					var options = new MqttClientOptionsBuilder()
						.WithTcpServer("localhost", 1883)
						.Build();

					await client.ConnectAsync(options, CancellationToken.None);
					await client.PublishAsync(message, CancellationToken.None);
					await client.DisconnectAsync();
					string messageToPrint = message.ConvertPayloadToString();
					Console.WriteLine("MQTT message is published:" + messageToPrint);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}
	}
}
