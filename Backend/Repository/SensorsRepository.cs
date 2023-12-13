using Backend.Dtos;
using Backend.Persistence;
using Backend.Persistence.Entities;

namespace Backend.Repository;

internal class SensorsRepository : ISensorsRepository<SensorDataEntity>
{
	private readonly MongoDbService _persistance;

	public SensorsRepository()
	{
		// _persistance = new MongoDbService("mongodb://host.docker.internal:27017", "sensors");
		_persistance = new MongoDbService("mongodb://localhost:27017", "sensors");
	}

	public List<SensorDataEntity> Get(FilterDto filterParams, SortDto sortParams)
		=> _persistance.GetSensorsData(filterParams, sortParams);
}
