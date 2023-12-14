using Backend.Broker;
using Backend.Persistence;
using Backend.Persistence.Entities;
using Backend.Repository;

await Task.Factory.StartNew(async () =>
{
	try
	{
		// TODO MS
		//var service = new MongoDbService("mongodb://localhost:27017", "sensors");
		var service = new MongoDbService("mongodb://host.docker.internal:27017", "sensors");
		var messageBroker = new MessageBroker(service);

		Console.WriteLine("Starting MessageBroker...");

		var topics = new List<string> { "sensors/temperature", "sensors/humidity", "sensors/pressure", "sensors/CO2" };

		var isConnected = false;

		while (!isConnected)
		{
			try
			{
				await messageBroker.SubscribeToTopics(topics);

				isConnected = true;				
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);

				isConnected = false;
				await Task.Delay(5000);
			}
		}

		Console.WriteLine("MessageBroker finished it's work");
	}
	catch (Exception ex)
	{
		Console.WriteLine(ex);
	}
});

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISensorsRepository<SensorDataEntity>, SensorsRepository>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: "CORS_POLICY", corsBuilder =>
	{
		corsBuilder.AllowAnyOrigin().AllowAnyHeader().Build();
	});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CORS_POLICY");

app.MapControllers().RequireCors("CORS_POLICY");

app.Run();