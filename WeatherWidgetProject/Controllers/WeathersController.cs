using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Web.Mvc;
using WeatherWidgetProject.ViewModels;

namespace WeatherProject.Controllers
{
    public class WeathersController : Controller
    {
        
        public ActionResult GetWeather()
        {
            return View();
        }




        [HttpPost]
        public ActionResult ViewWeather(RootObject rootObject)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Home");
            

            rootObject.Zipcodes = rootObject.Zipcode.Split(',');

            if (rootObject.Zipcodes.Length > 1)
            {
                for (int i = 0; i < rootObject.Zipcodes.Length; i++)
                {
                    HttpWebRequest apiRequest = WebRequest.Create($"http://api.wunderground.com/api/d9cca49bf824a20b/forecast10day/q/{rootObject.Zipcodes[i]}.json") as HttpWebRequest;
                    string apiResponse = "";
                    var zip = rootObject.Zipcodes[i];

                    if (i == 0)
                    {
                        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            apiResponse = reader.ReadToEnd();
                            rootObject.Weather1 = JsonConvert.DeserializeObject<Weather1>(apiResponse);
                            rootObject.Weather1.Zipcode = zip;
                        }
                    }

                    if (i == 1)
                    {
                        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            apiResponse = reader.ReadToEnd();
                            rootObject.Weather2 = JsonConvert.DeserializeObject<Weather2>(apiResponse);
                            rootObject.Weather2.Zipcode = zip;
                        }
                    }

                    if (i == 2)
                    {
                        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            apiResponse = reader.ReadToEnd();
                            rootObject.Weather3 = JsonConvert.DeserializeObject<Weather3>(apiResponse);
                            rootObject.Weather3.Zipcode = zip;
                        }
                    }

                    if (i == 3)
                    {
                        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            apiResponse = reader.ReadToEnd();
                            rootObject.Weather4 = JsonConvert.DeserializeObject<Weather4>(apiResponse);
                            rootObject.Weather4.Zipcode = zip;
                        }
                    }

                    if (i == 4)
                    {
                        using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                        {
                            var reader = new StreamReader(response.GetResponseStream());
                            apiResponse = reader.ReadToEnd();
                            rootObject.Weather5 = JsonConvert.DeserializeObject<Weather5>(apiResponse);
                            rootObject.Weather5.Zipcode = zip;
                        }
                    }
                }

                return View("ViewWeatherMulti", rootObject);
            }

            else
            {
                HttpWebRequest apiRequest = WebRequest.Create($"http://api.wunderground.com/api/d9cca49bf824a20b/forecast10day/q/{rootObject.Zipcode}.json") as HttpWebRequest;

                var zip = rootObject.Zipcode;
                string apiResponse;

                using (HttpWebResponse response = apiRequest.GetResponse() as HttpWebResponse)
                {
                    var reader = new StreamReader(response.GetResponseStream());
                    apiResponse = reader.ReadToEnd();
                    rootObject = JsonConvert.DeserializeObject<RootObject>(apiResponse);
                    rootObject.Zipcode = zip;
                }
                
                return View(rootObject);
            }
        }

    }
}