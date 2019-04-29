using MealPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MealPlanner.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Home");
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        public ActionResult Register(RegisterPageModel registerPageModel)
        {
            if (ModelState.IsValid)
            {


                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                userBusinessLayer.AddUser(registerPageModel.User);
                Session["id"]= userBusinessLayer.GetUserId(registerPageModel);
                
                
                registerPageModel.BodyStats.UserId =(int) Session["id"];
                BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
                bodyStatsBusinessLayer.AddBodyStats(registerPageModel.BodyStats);



            }

            return View();
        }

        public ActionResult SignIn()
        {
            return View("SignIn");
        }

        //test code for register in two steps
        #region


        //[HttpGet]
        //[ActionName("CreateUser")]
        //public ActionResult CreateUser()
        //{

        //    return View("CreateUser");
        //}

        //[HttpPost]
        ////using action method attibute
        //[ActionName("CreateUser")]
        //public ActionResult CreateUser(User user)
        //{


        //    if (ModelState.IsValid)
        //    {


        //        UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
        //        userBusinessLayer.AddUser(user);



        //    }

        //    return View();

        //}


        //[HttpGet]
        //[ActionName("AddBodyStatus")]
        //public ActionResult AddBodyStatus(int id)
        //{


        //    BodyStats bodyStats = new BodyStats();
        //    bodyStats.UserId = id;
        //    return View(bodyStats);
        //}

        //[HttpPost]
        ////using action method attibute
        //[ActionName("AddBodyStatus")]
        //public ActionResult AddBodyStatus(BodyStats bodyStats)
        //{



        //    if (ModelState.IsValid)
        //    {

        //        BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
        //        bodyStatsBusinessLayer.AddBodyStats(bodyStats);


        //        return RedirectToAction("BodyStats", new { id = bodyStats.UserId });


        //    }

        //    return View();

        //}

        #endregion

    }
}