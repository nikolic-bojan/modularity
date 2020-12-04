using System.Collections.Generic;

namespace WeatherForecast.Core
{
    public interface IWeatherForecastService
    {
        IEnumerable<WeatherForecastDto> Get();
    }
}