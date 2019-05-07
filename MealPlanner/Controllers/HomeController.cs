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
        [HttpGet]
        [ActionName("Register")]
        public ActionResult Register()
        {
            return View("Register");
        }
        
        [HttpPost]
        [ActionName("Register")]
        public ActionResult RegisterSave()
        {
            if (int.TryParse(Request["daysToGoal"], out int daysToGoal) && int.TryParse(Request["weightGoal"], out int weightGoal)
                && int.TryParse(Request["heightFeet"], out int heightFeet) && int.TryParse(Request["heightInches"], out int heightInches)
                && int.TryParse(Request["weight"], out int weight) && int.TryParse(Request["age"], out int age))
            {
                User u = new User
                {
                    FirstName = Request["fname"],
                    LastName = Request["lname"],
                    Email = Request["email"],
                    Password = Request["password"],
                    ConfirmPassword = Request["confirmPassword"],
                    BodyStats = new BodyStats
                    {
                        ActivityLevel = (Models.Enums.ActivityLevel)Enum.Parse(typeof(Models.Enums.ActivityLevel), Request["activityLevel"]),

                        TargetDays = daysToGoal,
                        TargetWeight = weightGoal,
                        HeightFeet = heightFeet,
                        HeightInches = heightInches,
                        Weight = weight,
                        Age = age,
                        // Gender = Convert.ToBoolean(Convert.ToInt32(Request["gender"])),
                        Gender = (Models.Enums.Gender)Enum.Parse(typeof(Models.Enums.Gender), Request["gender"]),

                        LoseOrMaintainWeight = Convert.ToBoolean(Convert.ToInt32(Request["loseOrMaintainWeight"]))
                    }
                };
                if (Database.AddUserToDatabase(u))
                {
                    if (Database.AddBodyStats(u))
                    {
                        return Json(new { Status = (int)HttpStatusCode.OK });

                    }
                }
                return Json(new { Status = (int)HttpStatusCode.InternalServerError });
            }
            else
            {
                return Json(new { Status = (int)HttpStatusCode.BadRequest });
            }

        }
        public ActionResult SignIn()
        {
            return View("SignIn");
        }
        [HttpGet]
        public ActionResult SignIn(string userid, string password)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { Status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }

            if (Database.ValidateUser(userid, password))
            {
                HttpContext.Session["UserId"] = userid;
                return Json(new { Status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Status = (int)HttpStatusCode.Unauthorized }, JsonRequestBehavior.AllowGet);
            }
        }

        public void SignOut()
        {

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