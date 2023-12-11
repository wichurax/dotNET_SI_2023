using Backend.Dtos;
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
			// TODO MS

			var result = new List<SensorMeasurementDto>();

			result.Add(new SensorMeasurementDto()
			{
				MeasurementDate = DateTime.Now,
				SensorName = "XD",
				SensorType = "temperature"
			});

			return Ok(result);
		}
	}

	public class FilterDto
	{
		public DateTime? From { get; set; }
		public DateTime? To { get; set; }
		public string? SensorType { get; set; }
		public string? SensorName { get; set; }
	}

	public class SortDto
	{
		/// <summary>
		/// Name of column we want to sort data
		/// </summary>
		public string? ColumnName { get; set; }

		public SortDirection Direction { get; set; } = SortDirection.Ascending;
	}

	public enum SortDirection
	{
		Ascending,
		Descending
	}
}
