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

        [Route("/User/Register")]
        public ActionResult Register()
        {
            if (int.TryParse(Request["daysToGoal"], out int daysToGoal) && int.TryParse(Request["weightGoal"], out int weightGoal)
                && int.TryParse(Request["heightFeet"], out int heightFeet) && int.TryParse(Request["heightInches"], out int heightInches)
                && int.TryParse(Request["weight"], out int weight) && int.TryParse(Request["age"], out int age))
            {
                User u = new User
                {
                    fname = Request["fname"],
                    lname = Request["lname"],
                    email = Request["email"],
                    password = Request["password"],
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
                        return RedirectToAction("SignIn", "Home");
                        //return Json(new { Status = (int)HttpStatusCode.OK });

                    }
                }
                return RedirectToAction("SignIn", "Home");
                //return Json(new { Status = (int)HttpStatusCode.InternalServerError });
            }
            else
            {
                return RedirectToAction("SignIn", "Home");
                //return Json(new { Status = (int)HttpStatusCode.BadRequest });
            }

        }

        [Route("/User/SignIn")]
        public ActionResult SignIn(string userid, string password)
        {
            if (string.IsNullOrWhiteSpace(userid) || string.IsNullOrWhiteSpace(password))
            {
                Session["id"] = 1;
                //return Json(new { Status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "Dashboard");

            }

            if (Database.ValidateUser(userid, password))
            {
                //HttpContext.Session["UserId"] = userid;
                BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();

                // HttpContext.Session["UserId"] = bodyStatsBusinessLayer.GetUserIdBasedEmail(userid, password);
                Session["id"] = bodyStatsBusinessLayer.GetUserIdBasedEmail(userid, password);
                Session["mp"] = false; 
                // return Json(new { Status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
                return RedirectToAction("Index", "Dashboard");

            }
            //else
            //{
            //    return RedirectToAction("Index", "Dashboard", id);
            //    //return Json(new { Status = (int)HttpStatusCode.Unauthorized }, JsonRequestBehavior.AllowGet);
            //}
            return View();
        }



        public void SignOut()
        {
          
        }
    }
}