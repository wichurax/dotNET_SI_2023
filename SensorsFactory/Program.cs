﻿using System.Text.Json;
using Configuration;
using GenericSensorNamespace;
using MQTTnet;
using SensorsFactory;

public class Program
{
	static MessagePublisher publisher = new MessagePublisher();
	static SensorsConfiguration _configuration = null!;
	static DateTime _startDate = new DateTime(2023, 12, 1);
	static DateTime _endDate = new DateTime(2023, 12, 13);
	
    static async Task Main()
    {
		Random rand = new Random();
		List<GenericSensor> sensors = new List<GenericSensor>();
		_configuration = SensorsConfigurationService.Get();

		int temperatureSensorsAmount = _configuration.SensorsList.Where(sensor => sensor.Type == "temperature").Select(sensor => sensor.InstancesAmount).FirstOrDefault();
		int pressureSensorsAmount = _configuration.SensorsList.Where(sensor => sensor.Type == "pressure").Select(sensor => sensor.InstancesAmount).FirstOrDefault();
		int co2SensorsAmount = _configuration.SensorsList.Where(sensor => sensor.Type == "CO2").Select(sensor => sensor.InstancesAmount).FirstOrDefault();
		int humiditySensorsAmount = _configuration.SensorsList.Where(sensor => sensor.Type == "humidity").Select(sensor => sensor.InstancesAmount).FirstOrDefault();

		for(int i = 0; i < temperatureSensorsAmount; i++){
			GenericSensor newSensor = new("temperature");
			sensors.Add(newSensor);
		}
		for(int i = 0; i < pressureSensorsAmount; i++){
			GenericSensor newSensor = new("pressure");
			sensors.Add(newSensor);
		}
		for(int i = 0; i < co2SensorsAmount; i++){
			GenericSensor newSensor = new("CO2");
			sensors.Add(newSensor);
		}
		for(int i = 0; i < humiditySensorsAmount; i++){
			GenericSensor newSensor = new("humidity");
			sensors.Add(newSensor);
		}


        foreach (GenericSensor sensor in sensors){
			SendDataAsync(sensor);
			Console.WriteLine($"{sensor.SensorName} has started");
			var delay = sensor.Interval * rand.NextDouble() * 1000;
			await Task.Delay((int)delay);
		}
		Console.ReadKey();
	}

    static async Task SendDataAsync(GenericSensor sensor)
    {
	    for (DateTime date = _startDate; date <= _endDate; date = date.AddDays(1))
	    {
			Double value = sensor.GenereteNextValue();
			String sensorType = sensor.SensorType;

			var data = new SensorData(sensorType, sensor.SensorName, value, sensor.Unit, date);
			var serializedData = JsonSerializer.Serialize(data);
			
			var message = new MqttApplicationMessageBuilder()
				.WithTopic("sensors/" + sensorType)
				.WithPayload(serializedData)
				.Build();
						
			await publisher.Publish(message);
			await Task.Delay((int)sensor.Interval*1000);
		}
    }
}