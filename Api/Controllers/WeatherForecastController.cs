using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WeatherForecast.Core;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(IWeatherForecastService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<WeatherForecastDto> Get()
        {
            return _service.Get();
        }
    }
}
