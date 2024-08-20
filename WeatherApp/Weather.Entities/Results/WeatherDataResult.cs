namespace Weather.Entities.Results;


public class WeatherDataResult
{
    public double Temperature { get; set; }
    public int WindDirection { get; set; }
    public double WindSpeed { get; set; }
    public DateTime SunriseDateTime { get; set; }
    
}