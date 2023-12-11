namespace MessageBroker;

using MongoDB.Driver;

internal class MongoDbService
{
    private readonly IMongoCollection<SensorDataEntity> _sensorDataCollection;

    public MongoDbService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        database.CreateCollection("sensorsData");
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
    
    public List<SensorDataEntity> GetRecentData(int pageIndex, int pageSize)
    {
        var sortDefinition = Builders<SensorDataEntity>.Sort.Descending(x => x.MeasurementDate);
        var skip = (pageIndex - 1) * pageSize;

        return _sensorDataCollection.Find(FilterDefinition<SensorDataEntity>.Empty)
            .Sort(sortDefinition)
            .Skip(skip)
            .Limit(pageSize)
            .ToList();
    }
}
