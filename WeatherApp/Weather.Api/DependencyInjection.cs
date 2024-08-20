using HttpHandler;
using Weather.Application;
using Weather.Application.Services;
using Weather.Entities;
using Weather.Entities.Models;
using Weather.Entities.Results;
using Weather.Repository;

namespace Weather.Api;

public static class DependencyInjection
{
    public static void Configure(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddHttpClient<HttpRequestService>();
        service.AddTransient<WeatherService>();
        service.AddTransient<WeatherRepository>();
        service.AddSingleton<WeatherDatabaseSettings>(_ =>
        {
            var config = configuration.GetSection("WeatherDatabase").Get<WeatherDatabaseSettings>();
            return new WeatherDatabaseSettings
            {
                ConnectionString = config.ConnectionString,
                DatabaseName = config.DatabaseName,
                WeatherCollectionName = config.WeatherCollectionName
            };
        });

    }
}