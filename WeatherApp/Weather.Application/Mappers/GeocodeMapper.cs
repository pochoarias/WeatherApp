using Weather.Entities;
using Weather.Entities.Models;
using Weather.Entities.Response;

namespace Weather.Application.Mappers;

public static class GeocodeMapper
{
    public static GeocodeData MapToLocation(GeocodeResponse response)
    {
        if (response == null) throw new ArgumentNullException(nameof(response));

        return new GeocodeData
        {
            Latitude = response.Results.Count> 0 ? response.Results[0].Geometry.Location.Latitude : default,
            Longitude = response.Results.Count> 0 ? response.Results[0].Geometry.Location.Longitude : default,
        };
    }
}