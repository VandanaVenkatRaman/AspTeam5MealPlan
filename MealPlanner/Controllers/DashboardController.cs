using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MealPlanner.Models;



namespace MealPlanner.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            List<User> users = userBusinessLayer.Users.ToList();
           


            return View(users);
        }

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


        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
           User user = userBusinessLayer.Users.Single(u => u.UserID == id);


            return View(user);
        }




        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edit(User user)    
        {


            if (ModelState.IsValid)
            {
                UserBusinessLayer userBusinessLayer = new UserBusinessLayer();

                userBusinessLayer.SaveUser(user);
                return RedirectToAction("Index");
            }

            return View(user);

        }


        //right now delete funtion is not able to use since userID is the foreign key of physicalStats table

        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    //EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
        //    //employeeBusinessLayer.DeleteEmployee(Id);
        //    UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
        //    userBusinessLayer.DeleteUser(id);
            
        //    return RedirectToAction("Index");
        //}



        //
        public ActionResult PhysicalProfile()
        {
            return View("PhysicalProfile");
        }
    }
}