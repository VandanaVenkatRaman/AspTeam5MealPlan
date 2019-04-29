using MealPlanner.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using unirest_net.http;

namespace MealPlanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Home");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View("Register");
        }
        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        [HttpGet]
        public ActionResult DemoMealPlan()
        {
            //removed api-key
            HttpResponse<string> response = Unirest.get("https://spoonacular-recipe-food-nutrition-v1.p.rapidapi.com/recipes/mealplans/generate?timeFrame=day&targetCalories=2000")
                .header("X-RapidAPI-Host", "spoonacular-recipe-food-nutrition-v1.p.rapidapi.com")
                .header("X-RapidAPI-Key", "")
                .asJson<string>();

            var demo = JsonConvert.DeserializeObject(response.Body);

            ViewBag.Message = demo;
            return View("DemoMealPlan");

        }
    }
}