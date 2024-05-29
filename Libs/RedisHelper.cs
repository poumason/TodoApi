using Microsoft.Extensions.Options;
using TodoApi.Models;
using StackExchange.Redis;

namespace TodoApi.Libs
{
    public interface IRedisHelper
    {
        Task<string> Get(string key);

        Task<string> Set(string key, string value);
    }

    public class RedisHelper: IRedisHelper
    {
        private readonly ILogger _logger;
        private readonly AppConfig _config;
        private readonly IDatabase _db;

        public RedisHelper(IOptions<AppConfig> config, ILogger<RedisHelper> logger)
        {
            _config = config.Value;
            _logger = logger;
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_config.RedisHost);
            _db = redis.GetDatabase();
        }

        public async Task<string> Get(string key)
        {
            RedisKey _key = new RedisKey(key);
            var x = _db.KeyExists(key);
            var value = await _db.HashGetAsync(_key, "data");
            _logger.LogInformation($"get value from redis: {value}");
            return value.ToString();
        }

        public async Task<string> Set(string key, string value)
        {
            return (await _db.StringSetAndGetAsync(key, value)).ToString();
        }
    }
}