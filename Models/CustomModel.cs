using Microsoft.Extensions.Localization;

namespace TodoApi.Models
{
    public class CustomModel
    {
        private readonly IStringLocalizer<CustomModel> _localizer;

        public CustomModel()
        {
        }

        public CustomModel(IStringLocalizer<CustomModel> localizer)
        {
            _localizer = localizer;
        }

        public string GetHello() {
            return _localizer["hello"];
        }
    }
}