using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TodoApi.Libs;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _defLogger;
    private readonly CustomLogger _logger;
    private readonly IStringLocalizer<WeatherForecastController> _localizer;

    public WeatherForecastController(CustomLogger logger, IStringLocalizer<WeatherForecastController> locaizer, ILogger<WeatherForecastController> defLogger)
    {
        _logger = logger;
        _defLogger = defLogger;
        _localizer = locaizer;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    // private readonly ILogger<WeatherForecastController> _logger;

    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _logger.Log("========= invoking =======");
        _defLogger.LogInformation("info hello world");
        _defLogger.LogCritical("cirtical hello world");
        _defLogger.LogTrace("trace hello world");

        var testModel = new CustomModel(_localizer, _defLogger);
        _logger.Log($"{testModel.GetHello()} from model");

        _logger.Log(_localizer["hello"]);
        
        _logger.Log(_localizer["test"]);
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
