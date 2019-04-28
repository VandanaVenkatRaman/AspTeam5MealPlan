using MealPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MealPlanner.Controllers
{
    public class Register : Controller
    {
        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create_Get()
        {

            return View();
        }

        [HttpPost]
        //using action method attibute
        [ActionName("Create")]
        public ActionResult Create_Post(User user)
        {


            if (ModelState.IsValid)
            {


                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
                userBusinessLayer.AddUser(user);

                return RedirectToAction("Index");


            }

            return View();

        }


        //[HttpPost]
        //public void Register()
        //{

        //}

        [HttpPost]
        public void SignIn()
        {

        }

        public void SignOut()
        {

        }
    }
}