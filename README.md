# Weather Forecast REST API

This project implements a self-documenting REST API that retrieves weather data from an external service provider and stores it in a local MongoDB database. The API allows clients to query weather information based on location parameters, and once data is retrieved and stored, subsequent requests with the same parameters are served from the local database instead of querying the external API again.

## Endpoints

### 1. Get Weather by Location
- **Endpoint**: `/api/weather/location`
- **Method**: GET
- **Parameters**:
  - `latitude`: (required) Latitude of the location.
  - `longitude`: (required) Longitude of the location.
- **Response**:
  - `temperature`: Current temperature.
  - `windDirection`: Current wind direction.
  - `windSpeed`: Current wind speed.
  - `sunrise`: Time of sunrise.
  - `dateTime`: Date and time of the weather data.
  
### 2. Get Weather by City (Bonus)
- **Endpoint**: `/api/weather/city`
- **Method**: GET
- **Parameters**:
  - `city`: (required) Name of the city.
- **Response**:
  - `temperature`: Current temperature.
  - `windDirection`: Current wind direction.
  - `windSpeed`: Current wind speed.
  - `sunrise`: Time of sunrise.
  - `dateTime`: Date and time of the weather data.

*Note: The data for these fields is fetched based on the latitude and longitude of the city if available. This endpoint requires an Google API KEY to be configured in the app.*

## Objective

The main objective is to minimize calls to the external weather API by utilizing MongoDB as a local storage cache. Only the first request with a specific set of parameters (location or city) will reach out to the external API. Subsequent requests with the same parameters will return data from the local MongoDB storage.

## Technologies Used

- **.NET 6**: Backend framework to implement the REST API.
- **MongoDB**: Database used for local storage of weather data.
- **Swagger/OpenAPI**: For self-documenting the API.

## Developer Notes

- The MongoDB connection string should be configured for localhost.
- All API responses should be in JSON format.
- The REST API should implement OpenAPI specifications using Swagger for documentation.

## External Weather Service

The weather data is sourced from [Open-Meteo.com](https://open-meteo.com/en/docs).
