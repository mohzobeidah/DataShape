using System.Dynamic;
using dataShaper.DataShape;
using Microsoft.AspNetCore.Mvc;

namespace dataShaper.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDataShaper<WeatherForecast> _dataShaper;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDataShaper<WeatherForecast> dataShaper)
    {
        this._dataShaper = dataShaper;
        _logger = logger;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

   // [HttpGet(Name = "GetWeatherForecastWithParams")]
    public IEnumerable<ExpandoObject> GetWithParams(string fields)
    {
        var xx= Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
       return  _dataShaper.ShapeData(xx,fields);
    }
}
