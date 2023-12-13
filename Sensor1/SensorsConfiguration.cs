using System.Text.Json;

namespace Configuration;

internal class SensorsConfiguration
{
	public ICollection<SensorConfiguration> SensorsList { get; set; } = null!;
}

internal class SensorConfiguration
{
	public string Type { get; set; } = null!;
	public string Unit { get; set; } = null!;
	public Double MaxValue { get; set; } = 0.0;
	public Double MinValue { get; set; } = 0.0;
	public Double Interval { get; set; } = 0.0;
	public int InstanecesAmount { get; set; } = 0;				
}

internal class SensorsConfigurationService
{
	private const string _configFileName = "sensors-config.json";

	public static SensorsConfiguration Get()
	{
		var configPath = Path.Combine(Directory.GetCurrentDirectory(), _configFileName);
		var json = File.ReadAllText(configPath);

		var configuration = JsonSerializer.Deserialize<SensorsConfiguration>(json);

		if (configuration == null)
			throw new Exception("Sensors configuration not found");

		return configuration;
	}
}
