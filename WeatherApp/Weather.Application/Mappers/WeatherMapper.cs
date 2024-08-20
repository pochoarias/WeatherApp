using Weather.Entities.Models;
using Weather.Entities.Response;
using Weather.Entities.Results;

namespace Weather.Application.Mappers;

public static class WeatherMapper
{
    public static WeatherDataResult MapToWeatherData(WeatherDataResponse response)
    {
        if (response == null) throw new ArgumentNullException(nameof(response));

        return new WeatherDataResult
        {
            Temperature = response.Current.Temperature2m,
            WindSpeed = response.Current.WindSpeed10m,
            WindDirection = response.Current.WindDirection10m,
            SunriseDateTime = response.Daily.Sunrise.Length > 0 ? DateTime.Parse(response.Daily.Sunrise[0]) : default
        };
    }
}