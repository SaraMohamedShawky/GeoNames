using GeonameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace GeonameAPI.Controllers
{
    public class SearchCitiesController : ApiController
    {
        private GeonameAPIContext db = new GeonameAPIContext();
        [Route("api/SearchCities/GetCities")]
        [HttpGet]
        public HttpResponseMessage GetCities(string term = null, string longtitude = null, string lititude = null)
        {
            if (term == null || term == "")
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You must enter search text");
            }
            else
            {
                using (var context = new GeonameAPIContextEntities())
                {

                    List<GeoName> Suggestions = context.GetSuggestedCities(term).ToList();
                    List<JsonSearchCities> citiesList = new List<JsonSearchCities>();
                    List<JsonSearchCities> nearest = new List<JsonSearchCities>();
                    const float locationscore = 0.2f;
                    if (Suggestions.Count != 0)
                    {
                        // add cities to list with score 
                        foreach (var city in Suggestions)

                        {
                            JsonSearchCities jc = new JsonSearchCities();
                            jc.Score = float.Parse(DiceMatch(term, city.name).ToString());
                            jc.geonameid = city.geonameid;
                            jc.name = city.name;
                            jc.latitude = float.Parse(city.latitude.ToString());
                            jc.longitude = float.Parse(city.longitude.ToString());
                            citiesList.Add(jc);
                        }

                        // search optional by longitiude & latitude
                        if (longtitude != null && lititude != null)
                        {
                            var loc = NearestCities(citiesList, double.Parse(lititude), double.Parse(longtitude));
                            foreach (var item in citiesList)
                            {
                                if (loc.Any(cus => cus.geonameid == item.geonameid))
                                {
                                    item.Score = item.Score + locationscore;
                                }
                            }

                        }


                    }

                    return Request.CreateResponse(citiesList.OrderByDescending(p => p.Score));
                }
            }
            //switch (gender.ToLower())
            //{
            //    case "all":
            //        return Request.CreateResponse(products.ToList());
            //case "male":
            //  return Request.CreateResponse(products.Where(e => e.Name == "Hammer").ToList());
            ////case "female":
            ////    return Request.CreateResponse(contentEntities.Employees.Where(e => e.Gender == "female").ToList());
            //default:
            //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Value for gender must be Male,Female or All." + gender +
            //                                           " is invalid.");
            //}
        }

        //Get score using Dice Coefficient algorithm
        private static double DiceMatch(string string1, string string2)
        {
            if (string.IsNullOrEmpty(string1) || string.IsNullOrEmpty(string2))
                return 0;

            if (string1 == string2)
                return 1;

            int strlen1 = string1.Length;
            int strlen2 = string2.Length;

            if (strlen1 < 2 || strlen2 < 2)
                return 0;

            int length1 = strlen1 - 1;
            int length2 = strlen2 - 1;

            double matches = 0;
            int i = 0;
            int j = 0;

            while (i < length1 && j < length2)
            {
                string a = string1.Substring(i, 2);
                string b = string2.Substring(j, 2);
                int cmp = string.Compare(a, b);

                if (cmp == 0)
                    matches += 2;

                ++i;
                ++j;
            }
            double value = matches / (length1 + length2);
            return Math.Round(value, 2);
        }

        //Get Nearest longitiude and latitiude
        private static List<JsonSearchCities> NearestCities(List<JsonSearchCities> citiesList, double lititude, double longtitude)
        {
            var constValue = 57.2957795130823D;

            var constValue2 = 3958.75586574D;



            var loc = (from l in citiesList
                       let temp = Math.Sin(Convert.ToDouble(l.latitude) / constValue) * Math.Sin(Convert.ToDouble(lititude) / constValue) +
                                                        Math.Cos(Convert.ToDouble(l.latitude) / constValue) *
                                                        Math.Cos(Convert.ToDouble(lititude) / constValue) *
                                                        Math.Cos((Convert.ToDouble(longtitude) / constValue) - (Convert.ToDouble(l.longitude) / constValue))
                       let calMiles = (constValue2 * Math.Acos(temp > 1 ? 1 : (temp < -1 ? -1 : temp)))
                       where (l.latitude > 0 && l.longitude > 0)
                       orderby calMiles

                       select new JsonSearchCities
                       {
                           geonameid = l.geonameid,
                           name = l.name,
                           longitude = l.longitude,
                           latitude = l.latitude,
                           Score = l.Score
                       });
            return loc.ToList();
        }

    }

}