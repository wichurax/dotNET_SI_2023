using Backend.Broker;
using Backend.Persistence;
using Backend.Persistence.Entities;
using Backend.Repository;

await Task.Factory.StartNew(async () =>
{
	var service = new MongoDbService("mongodb://localhost:27017", "sensors");
	var messageBroker = new MessageBroker(service);

	Console.WriteLine("Starting MessageBroker...");

	var topics = new List<string>
	{
		"sensors/temperature",
		"sensors/humidity",
		"sensors/pressure",
		"sensors/wind_speed"
	};

	await messageBroker.SubscribeToTopics(topics);

	Console.WriteLine("MessageBroker finished it's work");
});

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISensorsRepository<SensorDataEntity>, SensorsRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
