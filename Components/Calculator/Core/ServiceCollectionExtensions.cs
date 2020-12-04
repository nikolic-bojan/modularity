using Calculator.Core;
using Calculator.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace WeatherForecast.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddComponentCalculator(this IServiceCollection services)
        {
            return services.AddScoped<ICalculatorService, CalculatorService>();
        }
    }
}
