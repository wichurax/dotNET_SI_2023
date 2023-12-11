namespace SensorsFactory;

internal class SensorData
{
    public string SensorType { get; set; }
    public string SensorName { get; set; }
    public double Value { get; set; }
    public string Unit { get; set; }
    public DateTime MeasurementDate { get; set; }
    
    public SensorData(string sensorType, string sensorName, double value, string unit, DateTime measurementDate)
    {
        SensorType = sensorType;
        SensorName = sensorName;
        Value = value;
        Unit = unit;
        MeasurementDate = measurementDate;
    }
}