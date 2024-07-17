using System.Reflection;

namespace TodoApi;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    public string ToLog()
    {
        List<string> fields = new();
        foreach (var prop in this.GetType().GetProperties())
        {
            fields.Add($"{prop.Name}={prop.GetValue(this)}");
        }

        return string.Join(",", fields);
    }
}
