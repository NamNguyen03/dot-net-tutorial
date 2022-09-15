using System.Threading.Tasks;
using Weather.Models;

namespace Weather.Services
{
    public interface IWeatherService
    {
        MyResponseListModels<WeatherModel> get();
    }
}