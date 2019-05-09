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
        //output user table info
        public ActionResult Index(int id)
        {
            UserBusinessLayer  userBusinessLayer = new UserBusinessLayer();
            User user=  userBusinessLayer.GetUser(id);

            return View(user);
        }

        // Edit user table info

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
                return RedirectToAction("Index", new { id = user.userid });
            }

            return View(user);

        }

        
        //output BodyStats table

        public ActionResult BodyStats(int id)
        {
            
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            List<BodyStats> bodyStatss = bodyStatsBusinessLayer.GetBodyStats(id);
            ViewBag.data = id;
           

            return View(bodyStatss);
        }


        [HttpGet]
        [ActionName("AddBodyStatus")]
        public ActionResult AddBodyStatus(int id)
        {
            //ViewBag.data = id;
            BodyStats bodyStats = new BodyStats();
            bodyStats.UserId = id;
            return View(bodyStats);
        }

        [HttpPost]
        //using action method attibute
        [ActionName("AddBodyStatus")]
        public ActionResult AddBodyStatus( BodyStats bodyStats)
        {
            
           

            if (ModelState.IsValid)
            {

                BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
                bodyStatsBusinessLayer.AddBodyStats(bodyStats);

                
                return RedirectToAction("BodyStats", new { id = bodyStats.UserId });


            }

            return View();

        }



        [HttpGet]
        [ActionName("EditBodyStats")]
        public ActionResult EditBodyStats(int UserId, int id)
        {
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();

         

            BodyStats bodyStats = bodyStatsBusinessLayer.GetBodyStats(UserId).Single(b => b.BodyStatId == id);

            return View(bodyStats);
        }




        [HttpPost]
        [ActionName("EditBodyStats")]
        public ActionResult EditBodyStats(BodyStats bodyStats)
        {


            if (ModelState.IsValid)
            {
                BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();


                bodyStatsBusinessLayer.SaveBodyStats(bodyStats);
                return RedirectToAction("BodyStats", new { id = bodyStats.UserId });
            }

            return View(bodyStats);

        }



        //delete one record of bodyStats
        [HttpPost]
        public ActionResult DeletebodyStats(int id)
        {
            //EmployeeBusinessLayer employeeBusinessLayer = new EmployeeBusinessLayer();
            //employeeBusinessLayer.DeleteEmployee(Id);
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            bodyStatsBusinessLayer.DeleteBodyStats(id);
          
         

            return RedirectToAction("BodyStats", new { id = bodyStatsBusinessLayer.GetUserId(id) });
   
        }






    }
}