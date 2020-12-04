using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure;

namespace WeatherForecast.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddComponentWeatherForecast(this IServiceCollection services)
        {
            return services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        }
    }
}
