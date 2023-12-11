using Backend.Dtos;
using Backend.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
	[Route("api/sensors")]
	[ApiController]
	public class SensorsController : ControllerBase
	{
		private readonly ILogger<SensorsController> _logger;

		public SensorsController(ILogger<SensorsController> logger)
		{
			_logger = logger;
		}

		[HttpGet]
		public ActionResult<List<SensorMeasurementDto>> GetSensorMeasurement(
			[FromQuery] FilterDto filter, 
			[FromQuery] SortDto sort) 
		{
			var db = new MongoDbService("mongodb://localhost:27017", "sensors");

			var result = db.GetRecentData(sort)
				.Select(x => x.ToDto())
				.ToList();

			return Ok(result);
		}
	}
}
