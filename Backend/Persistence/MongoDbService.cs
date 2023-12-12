namespace Backend.Persistence;

using Backend.Dtos;
using Backend.Persistence.Entities;
using MongoDB.Driver;

internal class MongoDbService
{
	private readonly IMongoCollection<SensorDataEntity> _sensorDataCollection;

	public MongoDbService(string connectionString, string databaseName)
	{
		var client = new MongoClient(connectionString);
		var database = client.GetDatabase(databaseName);
		_sensorDataCollection = database.GetCollection<SensorDataEntity>("sensorsData");
	}

	public void InsertSensorData(SensorDataEntity sensorData)
	{
		_sensorDataCollection.InsertOne(sensorData);
	}

	public List<SensorDataEntity> GetSensorsData(FilterDto filterParams, SortDto sortParams) 
		=> _sensorDataCollection
			.AsQueryable()
			.ApplyFilters(filterParams)
			.ApplySort(sortParams)
			.ToList();
}

internal static class Extensions
{
	public static IQueryable<SensorDataEntity> ApplyFilters(this IQueryable<SensorDataEntity> entities, FilterDto filterParams)
	{
		if (filterParams.From != null)
			entities = entities.Where(x => x.MeasurementDate > filterParams.From);

		if (filterParams.To != null)
			entities = entities.Where(x => x.MeasurementDate < filterParams.To);

		if (filterParams.SensorType.Count > 0)
			entities = entities.Where(x => filterParams.SensorType.Contains(x.SensorType));

		if (filterParams.SensorName.Count > 0)
			entities = entities.Where(x => filterParams.SensorName.Contains(x.SensorName));

		return entities;
	}

	public static IQueryable<SensorDataEntity> ApplySort(this IQueryable<SensorDataEntity> entities, SortDto sortParams)
	{
		if (sortParams.ColumnName == null || sortParams.ColumnName == "")
			return entities;

		entities = sortParams.Direction == Enums.SortDirection.Descending
			? entities.SortDescendingBuilder(sortParams.ColumnName)
			: entities.SortAscendingBuilder(sortParams.ColumnName);

		return entities;
	}

	private static IQueryable<SensorDataEntity> SortDescendingBuilder(this IQueryable<SensorDataEntity> entities, string columnName)
	{
		return columnName switch
		{
			"SensorType" => entities.OrderByDescending(x => x.SensorType),
			"SensorName" => entities.OrderByDescending(x => x.SensorName),
			"Value" => entities.OrderByDescending(x => x.Value),
			"Unit" => entities.OrderByDescending(x => x.Unit),
			"MeasurementDate" => entities.OrderByDescending(x => x.MeasurementDate),
			_ => throw new ArgumentOutOfRangeException(nameof(columnName)),
		};
	}

	private static IQueryable<SensorDataEntity> SortAscendingBuilder(this IQueryable<SensorDataEntity> entities, string columnName)
	{
		return columnName switch
		{
			"SensorType" => entities.OrderBy(x => x.SensorType),
			"SensorName" => entities.OrderBy(x => x.SensorName),
			"Value" => entities.OrderBy(x => x.Value),
			"Unit" => entities.OrderBy(x => x.Unit),
			"MeasurementDate" => entities.OrderBy(x => x.MeasurementDate),
			_ => throw new ArgumentOutOfRangeException(nameof(columnName)),
		};
	}
}