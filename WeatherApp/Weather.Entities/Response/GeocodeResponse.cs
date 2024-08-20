using System.Text.Json.Serialization;

namespace Weather.Entities.Response;

public class GeocodeResponse
{
    [JsonPropertyName("results")]
    public List<Result> Results { get; set; }
    
    [JsonPropertyName("status")]
    public string Status { get; set; }
}

public class Result
{
    [JsonPropertyName("geometry")]
    public Geometry Geometry { get; set; }
}

public class Geometry
{
    [JsonPropertyName("location")]
    public Location Location { get; set; }
}

public class Location
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("lng")]
    public double Longitude { get; set; }
}

