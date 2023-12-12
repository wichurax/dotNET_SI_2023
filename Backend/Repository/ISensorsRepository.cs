using Backend.Dtos;
namespace Backend.Repository;

public interface ISensorsRepository<T>
{
	List<T> Get(FilterDto filterParams, SortDto sortParams);
}

