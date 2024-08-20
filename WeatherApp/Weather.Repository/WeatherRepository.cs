using MongoDB.Driver;
using Weather.Entities.Response;

namespace Weather.Repository;

public class WeatherRepository
{
    private readonly IMongoCollection<WeatherDataResponse> _weatherCollection;

    public WeatherRepository(WeatherDatabaseSettings weatherDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            weatherDatabaseSettings.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            weatherDatabaseSettings.DatabaseName);

        _weatherCollection = mongoDatabase.GetCollection<WeatherDataResponse>(
            weatherDatabaseSettings.WeatherCollectionName);
    }
    
    public async Task<WeatherDataResponse?> GetAsync(double latitude, double longitude) =>
        await _weatherCollection.Find(
                x =>
                    Math.Abs(x.Latitude - latitude) < 0.01 &&
                    Math.Abs(x.Longitude - longitude) < 0.01)
            .FirstOrDefaultAsync();

    public async Task CreateAsync(WeatherDataResponse newWeatherData) =>
        await _weatherCollection.InsertOneAsync(newWeatherData);

}