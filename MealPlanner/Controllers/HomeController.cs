﻿using System;
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
    }
}