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
        //public ActionResult Register(RegisterPageModel registerPageModel)
        //{
        //    if (ModelState.IsValid)
        //    {


        //        UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
        //        userBusinessLayer.AddUser(registerPageModel.User);
        //        Session["id"]= userBusinessLayer.GetUserId(registerPageModel);
                
                
        //        registerPageModel.BodyStats.UserId =(int) Session["id"];
        //        BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
        //        bodyStatsBusinessLayer.AddBodyStats(registerPageModel.BodyStats);



        //    }

        //    return View();
        //}

        public ActionResult SignIn()
        {
            return View("SignIn");
        }

  
    }
}