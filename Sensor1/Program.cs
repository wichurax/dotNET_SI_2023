// See https://aka.ms/new-console-template for more information

using MQTTnet;
using Sensor1;
using SensorsFactory;

// TODO MS

// potrzebuję żeby argumenty, które przyjmuje ta aplikacja konsolowa pozwalały na uruchomienie wielu instancji czujników
// najlepiej aby uruchomiona bez parametrów odpalała 16 instncji (4x4)
// częstotliwość wysyłki danych powinna być określona w konfiguracji (appsettings.json)
// możliwość uruchomienia wybranej ilośći instancji wybranego typu czujnika z wybraną częstotliwościa

// przykłady komend

// * SensorsFactory.exe
// program uruchamia się z domyślnymi parametrami

// * SensorsFactory.exe -t --instances 7 --interval 10
// -t oznacza temteraturę
// --instances 7 oznacza, że 7 instancji się ma uruchomić
// --interval 10 oznacza, że co 10 sekund ma być wysyłana nowa wartość 
// kolejne instancje uruchamiają się z opóźnieniem wg ciągu fibonacciego


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
