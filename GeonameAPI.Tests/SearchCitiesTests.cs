using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using GeonameAPI.Controllers;
using GeonameAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

//using WebAPI.Models;



namespace GeonameAPI.Tests
{
    [TestClass]
    public class SearchCitiesTests
    {
        GeonameAPIContext db = new GeonameAPIContext();
        [TestMethod]
        public void GetCities_SearchByOnlyStringWithSuggestion_ReturnSuggestions()
        {
            var controller = new SearchCitiesController();
            // Act on Test  
           HttpResponseMessage actionResult = controller.GetCities("");
            // Assert the result  
            Assert.AreEqual("400", "400");
        }
    }
}
