using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace TodoApi.Libs
{
    /// <summary>
    /// <seealso cref="https://github.com/iammukeshm/aspnet-core-json-localization?tab=readme-ov-file"/>
    /// </summary>
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cache;

        public JsonStringLocalizerFactory(IDistributedCache cache)
        {
            _cache = cache;
        }
        public IStringLocalizer Create(Type resourceSource) =>
            new JsonStringLocalizer(_cache);

        public IStringLocalizer Create(string baseName, string location) =>
            new JsonStringLocalizer(_cache);
    }
}