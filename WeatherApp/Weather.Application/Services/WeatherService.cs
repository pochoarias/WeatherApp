using System.Globalization;
using HttpHandler;
using Weather.Application.Mappers;
using Weather.Entities.Models;
using Weather.Entities.Response;
using Weather.Entities.Results;
using Weather.Repository;

namespace Weather.Application.Services;

public class WeatherService
{
    private readonly WeatherRepository _weatherRepository;
    private readonly HttpRequestService _requestService;
    private const string WeatherUrl = "https://api.open-meteo.com/v1/forecast";
    private const string GeocodeUrl = "https://maps.googleapis.com/maps/api/geocode/json";
    private const string GeocodeApi = "API_KEY";
    
    public WeatherService(WeatherRepository weatherRepository, HttpRequestService requestService)
    {
        _weatherRepository = weatherRepository;
        _requestService = requestService;
    }


    public async Task<WeatherDataResult?> GetWeather(GeocodeData geocodeData)
    {
        var local = await _weatherRepository.GetAsync(geocodeData.Latitude, geocodeData.Longitude);
        if (local != default)
        {
            var map = WeatherMapper.MapToWeatherData(local);
            return map;
        }

        var remote = await GetRemoteAsync(geocodeData);
        if (remote != default)
        {
            await CreateAsync(remote);
            var map = WeatherMapper.MapToWeatherData(remote);
            return map;
        } 
        
        return default;
    }
    
    public async Task<WeatherDataResult?> GetWeather(string city)
    {

        var location = await  GetGeocodeAsync(city);
        if (location is not { Status: "OK" }) return default;
            
        var geoPoint = GeocodeMapper.MapToLocation(location);
        var local = await _weatherRepository.GetAsync(geoPoint.Latitude, geoPoint.Longitude);
        if (local != default)
        {
            var map = WeatherMapper.MapToWeatherData(local);
            return map;
        }

        var remote = await GetRemoteAsync(geoPoint);
        if (remote != default)
        {
            await CreateAsync(remote);
            var map = WeatherMapper.MapToWeatherData(remote);
            return map;
        } 
        
        return default;
    }

    private async Task CreateAsync(WeatherDataResponse weatherData)
    {
        await _weatherRepository.CreateAsync(weatherData);
    }

    private async Task<WeatherDataResponse?> GetRemoteAsync(GeocodeData geocodeData)
    {
        var parameter = $"?latitude={geocodeData.Latitude.ToString("F4",CultureInfo.InvariantCulture)}" +
                        $"&longitude={geocodeData.Longitude.ToString("F4",CultureInfo.InvariantCulture)}" +
                        $"&current=temperature_2m,wind_speed_10m,wind_direction_10m&daily=sunrise&forecast_days=1";
        var request = await _requestService.SendAsync<WeatherDataResponse>(WeatherUrl, parameter, HttpMethod.Get);
        return request ?? default;
    }
    
    private async Task<GeocodeResponse?> GetGeocodeAsync(string city)
    {
        if (string.IsNullOrEmpty(GeocodeApi)) return default;
        
        var parameter = $"?address={city}&key={GeocodeApi}";
        var request = await _requestService.SendAsync<GeocodeResponse>(GeocodeUrl, parameter, HttpMethod.Get);
        return request ?? default;
    }
}