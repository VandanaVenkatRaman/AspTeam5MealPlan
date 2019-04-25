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
            User u = new User
            {
                FirstName = Request["fname"],
                LastName = Request["lname"],
                Age = int.Parse(Request["age"]),
                Email = Request["email"],
                Password = Request["password"],
                ConfirmPassword = Request["confirmPassword"],
                Gender = Convert.ToBoolean(Convert.ToInt32(Request["gender"])),
                BodyStats = new BodyStats
               {
                    ActivityLevel = Request["activityLevel"],
                    DaysToGoal = int.Parse(Request["daysToGoal"]),
                   WeightGoal = int.Parse(Request["weightGoal"]),
                   HeightFeet = int.Parse(Request["heightFeet"]),
                   HeightInches = int.Parse(Request["heightInches"]),
                   Weight = int.Parse(Request["weight"]),
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

        [HttpPost]
        public ActionResult SignIn(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return Json(new { Status = (int)HttpStatusCode.BadRequest });
            }

            if (ValidateUsernamePassword(username, password))
            {
                return View("UserDashboard");
            }
            else
            {
                return Json(new { Status = (int)HttpStatusCode.Unauthorized });
            }
        }

        private bool ValidateUsernamePassword(string username, string password)
        {
            return true;
        }

        public void SignOut()
        {

        }
    }
}