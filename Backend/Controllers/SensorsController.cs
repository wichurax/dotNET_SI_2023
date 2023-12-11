using Backend.Dtos;
using Microsoft.AspNetCore.Http;
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
		public ActionResult<SensorMeasurementDto> GetSensorMeasurement(FilterDto filter, SortDto sort) 
		{
			// TODO MS

			return new Ok();
		}
	}

	public class FilterDto
	{
		public DateTimeOffset? From { get; set; }
		public DateTimeOffset? To { get; set; }
		public string SensorType { get; set; }
		public string SensorName { get; set; }
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
