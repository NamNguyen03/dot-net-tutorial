using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Weather.Models;
using Weather.Services;

namespace Weather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;

        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpGet]
        public MyResponseListModels<WeatherModel> Get()
        {
            var rp = _weatherService.get();
            if (rp != null)
            {
                return rp;
            }
            return new MyResponseListModels<WeatherModel>
            {
                Data = new List<WeatherModel>(),
                Message = "call API fails",
                StatusCode = 400
            };
        }
    }
}
