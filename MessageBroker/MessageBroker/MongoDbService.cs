namespace MessageBroker;

using MongoDB.Driver;

public class MongoDbService
{
    private readonly IMongoCollection<SensorData> _sensorDataCollection;

    public MongoDbService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        database.CreateCollection("sensorsData");
        _sensorDataCollection = database.GetCollection<SensorData>("sensorsData");
    }

    public void InsertSensorData(SensorData sensorData)
    {
        _sensorDataCollection.InsertOne(sensorData);
    }

    /* example functions for data retrieval */
    public List<SensorData> GetDataBySensorType(string sensorType)
    {
        var filter = Builders<SensorData>.Filter.Eq(x => x.SensorType, sensorType);
        return _sensorDataCollection.Find(filter).ToList();
    }
    
    public List<SensorData> GetRecentData(int pageIndex, int pageSize)
    {
        var sortDefinition = Builders<SensorData>.Sort.Descending(x => x.MeasurementDate);
        var skip = (pageIndex - 1) * pageSize;

        return _sensorDataCollection.Find(FilterDefinition<SensorData>.Empty)
            .Sort(sortDefinition)
            .Skip(skip)
            .Limit(pageSize)
            .ToList();
    }
}
