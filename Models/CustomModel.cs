using Microsoft.Extensions.Localization;

namespace TodoApi.Models
{
    public class CustomModel
    {
        private readonly IStringLocalizer _localizer;
        private readonly ILogger _logger;

        public CustomModel(IStringLocalizer localizer, ILogger logger)
        {
            _localizer = localizer;
            _logger = logger;
        }

        public string GetHello() {
            _logger.LogInformation("from get hello");
            return _localizer["hello"];
        }
    }
}