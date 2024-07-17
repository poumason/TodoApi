using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TodoApi.Libs;

namespace TodoApi.Controllers;
[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;
    private readonly IStringLocalizer<HealthController> _localizer;

    private readonly IRedisHelper _redis;

    public HealthController(IStringLocalizer<HealthController> locaizer, ILogger<HealthController> logger, IRedisHelper redisHelper)
    {
        _localizer = locaizer;
        _logger = logger;
        _redis = redisHelper;
    }

    [HttpGet]
    public async Task<string> Get(string key)
    {
        return await _redis.Get(key);
    }

    [HttpPost]
    public async Task<string> Write(string key, string value)
    {
        return await _redis.Set(key, value);
    }
}
