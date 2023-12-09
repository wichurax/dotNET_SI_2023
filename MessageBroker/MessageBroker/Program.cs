using MessageBroker;

var service = new MongoDbService("mongodb://localhost:27017", "sensors");
var messageBroker = new MessageBroker.MessageBroker(service);
Console.WriteLine("Starting program MessageBroker");
await messageBroker.SubscribeToTopic("sensors/temperature");
Console.WriteLine("MessageBroker program finished");
