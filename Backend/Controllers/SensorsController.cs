using Backend.Dtos;
using Backend.Persistence.Entities;
using Backend.Repository;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using System.Text;

namespace Backend.Controllers;

[Route("api/sensors")]
[ApiController]
public class SensorsController : ControllerBase
{
	private readonly ISensorsRepository<SensorDataEntity> _repository;

	public SensorsController(ISensorsRepository<SensorDataEntity> repository)
	{
		_repository = repository;
	}

	[HttpGet("measurements")]
	public ActionResult<List<SensorMeasurementDto>> GetMeasurements([FromQuery] FilterDto filter, [FromQuery] SortDto sort)
	{
		var result = _repository
			.Get(filter, sort)
			.Select(x => x.ToDto())
			.ToList();

		return Ok(result);
	}

	[HttpGet("measurements-csv")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(typeof(FileContentResult), 200)]
	public IActionResult GetMeasurementsCsv([FromQuery] FilterDto filter, [FromQuery] SortDto sort)
	{
		var measurements = _repository.Get(filter, sort);
		
		var measurementsBytes = Encoding.ASCII.GetBytes(measurements.ToCsv());
		var fileName = $"measurements_csv_{DateTimeOffset.UtcNow:o}.csv";

		return File(measurementsBytes, "text/csv", fileName);
	}
}