using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace Weather.Entities.Response;

[BsonIgnoreExtraElements]
public class WeatherDataResponse
{

    [JsonPropertyName("longitude")] public double Longitude { get; set; }

    [JsonPropertyName("latitude")] public double Latitude { get; set; }

    [JsonPropertyName("current")] public CurrentData Current { get; set; }

    [JsonPropertyName("daily")] public DailyData Daily { get; set; }

}

public class CurrentData
{
    [JsonPropertyName("temperature_2m")] public double Temperature2m { get; set; }

    [JsonPropertyName("wind_speed_10m")] public double WindSpeed10m { get; set; }

    [JsonPropertyName("wind_direction_10m")]
    public int WindDirection10m { get; set; }
}

public class DailyData
{
    [JsonPropertyName("sunrise")] public string[] Sunrise { get; set; }
}