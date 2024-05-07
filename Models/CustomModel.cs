using Microsoft.Extensions.Localization;

namespace TodoApi.Models
{
    public class CustomModel
    {
        private readonly IStringLocalizer _localizer;

        public CustomModel(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public string GetHello() {
            return _localizer["hello"];
        }
    }
}