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
        //id should be stored in session state
        public ActionResult Index(int id)
        {
            UserBusinessLayer  userBusinessLayer = new UserBusinessLayer();
            User user=  userBusinessLayer.GetUser(id);

            return View(user);
        }



        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
          
            User user = userBusinessLayer.GetUser(id);
        

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
                return RedirectToAction("Index", new { id = user.UserID });
            }

            return View(user);

        }



        public ActionResult DisplayBodyStats(int id)
        {
            
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            List<BodyStats> bodyStatss = bodyStatsBusinessLayer.GetBodyStats(id);


            return View(bodyStatss);
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