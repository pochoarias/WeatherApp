using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Weather.Entities;
using Weather.Application;
using Weather.Application.Services;
using Weather.Entities.Models;


namespace Weather.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly WeatherService _weatherService; 
    
    public WeatherController(ILogger<WeatherController> logger, WeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("Location")]
    [SwaggerOperation(Summary = "Gets the weather data for a specific location.")]
    [SwaggerResponse(200, "Returns the weather data for the specified location.")]
    [SwaggerResponse(400, "If the location is null or invalid.")]
    [SwaggerResponse(500, "Unexpected error.")]
    public async Task<IActionResult> Get([FromQuery] GeocodeData location )
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        try
        {
            var result = await _weatherService.GetWeather(location);
            return Ok(result);
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Error, exc, "Something went wrong");
            return StatusCode(500, "Internal server error");
        }
    }
    
    [HttpGet("City")]
    [SwaggerOperation(Summary = "Gets the weather data for a specific city.")]
    [SwaggerResponse(200, "Returns the weather data for the specified city.")]
    [SwaggerResponse(400, "If the city is null or invalid.")]
    [SwaggerResponse(500, "Unexpected error.")]
    public async Task<IActionResult> Get([FromQuery] string name )
    {
        try
        {
            if (string.IsNullOrEmpty(name)) return BadRequest();
            
            var result = await _weatherService.GetWeather(name);
            return Ok(result);
        }
        catch (Exception exc)
        {
            _logger.Log(LogLevel.Error, exc, "Something went wrong");
            return StatusCode(500, "Internal server error");
        }
    }
}