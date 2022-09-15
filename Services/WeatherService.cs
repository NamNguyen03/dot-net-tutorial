using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Weather.Models;
using Newtonsoft.Json.Linq;
using System;

namespace Weather.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly string _url = "http://api.openweathermap.org/data/2.5/group";
        private readonly string _urlParameters = "?id=1580578,1581129,1581297,1581188,1587923&units=metric&appid=91b7466cc755db1a94caf6d86a9c788a";

        public WeatherService()
        {
            
        }
        
        public MyResponseListModels<WeatherModel> get()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(_url);
            List<WeatherModel> rs = new List<WeatherModel>();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            WeatherModel model =  new WeatherModel();
            HttpResponseMessage response = client.GetAsync(_urlParameters).Result; 
            if (response.IsSuccessStatusCode)
            {
                var dataObjects = response.Content.ReadAsStringAsync().Result; 
                JObject json = JObject.Parse(dataObjects);
                JObject jsonCity, jsonWeather, jsonMain;
                JArray jsonWeatherArr;
                 
                foreach (var e in json)
                {
                    if (e.Key == "list")
                    {
                        JArray arr = JArray.Parse(e.Value.ToString());
                        foreach (var item in arr)
                        {
                            jsonCity = JObject.Parse((item.ToString()));
                            foreach (var itemCity in jsonCity)
                            {
                               
                                if (itemCity.Key == "id")
                                {
                                    model.CityId = Int32.Parse(itemCity.Value.ToString());
                                    Console.WriteLine("id city: " + itemCity.Value);
                                }
                               
                                if (itemCity.Key == "name")
                                {
                                    model.CityName = itemCity.Value.ToString();
                                    rs.Add(model);
                                    Console.WriteLine("name city: " + itemCity.Value);
                                    Console.WriteLine("--------------------------------------------");
                                }

                                if (itemCity.Key == "weather")
                                {
                                    
                                    jsonWeatherArr = JArray.Parse(itemCity.Value.ToString());
                                    if (jsonWeatherArr.Count != 0)
                                    {
                                        jsonWeather = JObject.Parse(jsonWeatherArr[0].ToString());
                                        foreach (var itemWeather in jsonWeather)
                                        {
                                            if (itemWeather.Key == "main")
                                            {
                                                model = new WeatherModel();
                                                model.WeatherMain = itemWeather.Value.ToString();
                                                Console.WriteLine("weather main: " + itemWeather.Value);
                                            }

                                            if (itemWeather.Key == "description")
                                            {
                                                model.WeatherDescription = itemWeather.Value.ToString();
                                                Console.WriteLine("weather description: " + itemWeather.Value);
                                            }

                                            if (itemWeather.Key == "icon")
                                            {
                                                model.WeatherIcon = "http://openweathermap.org/img/wn/" + itemWeather.Value.ToString() + "@2x.png";
                                                Console.WriteLine("weather icon: "+ itemWeather.Value);
                                                 
                                            }
                                       
                                        }
                                    }
                                   
                                }

                                if (itemCity.Key == "main")
                                {
                                    jsonMain = JObject.Parse((itemCity.Value.ToString()));
                                    foreach (var itemMain in jsonMain)
                                    {
                                        if (itemMain.Key == "temp")
                                        {
                                            model.MainTemp = itemMain.Value.ToString();
                                            Console.WriteLine("Main temp: "+ itemMain.Value);
                                        }

                                        if (itemMain.Key == "humidity")
                                        {
                                            model.MainHumidity = itemMain.Value.ToString();
                                            Console.WriteLine("Main humidity: " + itemMain.Value);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                return null;
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
            
            return new MyResponseListModels<WeatherModel>
            {
                Data = rs,
                Message = "Current weather information of cities",
                StatusCode = 200
            };
        }
    }

}