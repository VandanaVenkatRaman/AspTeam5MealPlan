using MealPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MealPlanner.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register()
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
                    SingleBodyStats = new BodyStats
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
    }
}