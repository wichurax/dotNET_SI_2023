namespace Backend.Persistence;

using Backend.Dtos;
using Backend.Persistence.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Entities;
using System.Linq.Expressions;

internal class MongoDbService
{
	private readonly IMongoCollection<SensorDataEntity> _sensorDataCollection;

	public MongoDbService(string connectionString, string databaseName)
	{
		var client = new MongoClient(connectionString);
		var database = client.GetDatabase(databaseName);
		//database.CreateCollection("sensorsData");
		_sensorDataCollection = database.GetCollection<SensorDataEntity>("sensorsData");
	}

	public void InsertSensorData(SensorDataEntity sensorData)
	{
		_sensorDataCollection.InsertOne(sensorData);
	}

	/* example functions for data retrieval */
	public List<SensorDataEntity> GetDataBySensorType(string sensorType)
	{
		var filter = Builders<SensorDataEntity>.Filter.Eq(x => x.SensorType, sensorType);
		return _sensorDataCollection.Find(filter).ToList();
	}

	public List<SensorDataEntity> GetRecentData(SortDto sortParams)
	{
		return _sensorDataCollection
			.Find(new BsonDocument())
			.ApplySort(sortParams)
			.ToList();
	}
}

internal static class Extensions
{
	public static IFindFluent<SensorDataEntity, SensorDataEntity> ApplySort(
		this IFindFluent<SensorDataEntity, SensorDataEntity> fluentFind,
		SortDto sortParams)
	{
		if (sortParams.ColumnName == null || sortParams.ColumnName == "")
			return fluentFind;

		var sortDefinition2 = sortParams.Direction == Enums.SortDirection.Descending
			? Builders<SensorDataEntity>.Sort.Descending(x => x.Value)
			: Builders<SensorDataEntity>.Sort.Ascending(x => x.Value);

		var sortDefinition = sortParams.Direction == Enums.SortDirection.Descending
			? SortDescendingBuilder(sortParams.ColumnName)
			: SortAscendingBuilder(sortParams.ColumnName);

		return fluentFind.Sort(sortDefinition);
	}

	private static SortDefinition<SensorDataEntity> SortDescendingBuilder(string columnName)
	{
		return columnName switch
		{
			"SensorType" => Builders<SensorDataEntity>.Sort.Descending(x => x.SensorType),
			"SensorName" => Builders<SensorDataEntity>.Sort.Descending(x => x.SensorName),
			"Value" => Builders<SensorDataEntity>.Sort.Descending(x => x.Value),
			"Unit" => Builders<SensorDataEntity>.Sort.Descending(x => x.Unit),
			"MeasurementDate" => Builders<SensorDataEntity>.Sort.Descending(x => x.MeasurementDate),
			_ => throw new ArgumentOutOfRangeException(nameof(columnName)),
		};
	}

	private static SortDefinition<SensorDataEntity> SortAscendingBuilder(string columnName)
	{
		return columnName switch
		{
			"SensorType" => Builders<SensorDataEntity>.Sort.Ascending(x => x.SensorType),
			"SensorName" => Builders<SensorDataEntity>.Sort.Ascending(x => x.SensorName),
			"Value" => Builders<SensorDataEntity>.Sort.Ascending(x => x.Value),
			"Unit" => Builders<SensorDataEntity>.Sort.Ascending(x => x.Unit),
			"MeasurementDate" => Builders<SensorDataEntity>.Sort.Ascending(x => x.MeasurementDate),
			_ => throw new ArgumentOutOfRangeException(nameof(columnName)),
		};
	}
}