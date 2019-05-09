using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
        public ActionResult Index()
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();
            User user = userBusinessLayer.GetUser((int)Session["id"]);

            return View(user);
        }

        // Edit user table info

        [HttpGet]
        [ActionName("Edit")]
        public ActionResult Edit()
        {
            UserBusinessLayer userBusinessLayer = new UserBusinessLayer();

            
            User user = userBusinessLayer.GetUser((int)Session["id"]);
        

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

        
        //output BodyStats table

        public ActionResult BodyStats()
        {
            
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            List<BodyStats> bodyStatss = bodyStatsBusinessLayer.GetBodyStats((int)Session["id"]);
           
            return View(bodyStatss);
        }


        [HttpGet]
        [ActionName("AddBodyStatus")]
        public ActionResult AddBodyStatus()
        {
            //ViewBag.data = id;
            BodyStats bodyStats = new BodyStats
            {
                UserId = (int)Session["id"]
            };
            return View(bodyStats);
        }

        [HttpPost]
        //using action method attibute
        [ActionName("AddBodyStatus")]
        public ActionResult AddBodyStatus(BodyStats bodyStats)
        {
            if (ModelState.IsValid)
            {

                BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
                bodyStatsBusinessLayer.AddBodyStats(bodyStats);

                return RedirectToAction("BodyStats");
            }

            return View();

        }



        [HttpGet]
        [ActionName("EditBodyStats")]
        public ActionResult EditBodyStats(int bodyStatId)
        {
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            
            BodyStats bodyStats = bodyStatsBusinessLayer.GetBodyStats((int)Session["id"]).Single(b => b.BodyStatId == bodyStatId);

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
                return RedirectToAction("BodyStats");
            }

            return View(bodyStats);
        }



        //delete one record of bodyStats
        [HttpPost]
        public ActionResult DeletebodyStats(int id)
        {
            BodyStatsBusinessLayer bodyStatsBusinessLayer = new BodyStatsBusinessLayer();
            bodyStatsBusinessLayer.DeleteBodyStats(id);

            return RedirectToAction("BodyStats");
   
        }

        [Route("/Dashboard/SaveMealPlan")]
        public ActionResult SaveMealPlan(MealPlan m)
        {
            var bl = new UserBusinessLayer();

            var meals = new List<string>();

            foreach (var meal in m.Meals)
            {
                meals.Add(Meal.StringMeal(meal));
            }

            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveMenu", con)
                {
                    CommandType = CommandType.StoredProcedure
                };

                SqlParameter paramMealPlanId = new SqlParameter
                {
                    ParameterName = "@MealPlanId",
                    Value = m.MealPlanId
                };
                cmd.Parameters.Add(paramMealPlanId);

                SqlParameter paramUserId = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = (int)Session["id"]
                };
                cmd.Parameters.Add(paramUserId);

                SqlParameter paramBreakfast = new SqlParameter
                {
                    ParameterName = "@Breakfast",
                    Value = meals.ElementAt(0)
                };
                cmd.Parameters.Add(paramBreakfast);

                SqlParameter paramLunch = new SqlParameter
                {
                    ParameterName = "@Lunch",
                    Value = meals.ElementAt(1)
                };
                cmd.Parameters.Add(paramLunch);

                SqlParameter paramDinner = new SqlParameter
                {
                    ParameterName = "@Dinner",
                    Value = meals.ElementAt(2)
                };
                cmd.Parameters.Add(paramDinner);
                con.Open();

                cmd.ExecuteNonQuery();

                return RedirectToAction("Index");
            }
        }

        [Route("/Dashboard/LoadMealPlan")]
        public ActionResult LoadMealPlan(int mealId)
        {
            MealPlan m = new MealPlan();

            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spLoadMenu", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter paramMealId = new SqlParameter
                {
                    ParameterName = "@MealId",
                    Value = mealId
                };
                cmd.Parameters.Add(paramMealId);

                con.Open();


                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    m.MealPlanId = mealId;
                    m.Meals = new Meal[] {
                        Meal.ObjectMeal(rdr["Breakfast"].ToString()),
                        Meal.ObjectMeal(rdr["Lunch"].ToString()),
                        Meal.ObjectMeal(rdr["Dinner"].ToString())
                    };
                }
            }

            return View(m);
        }

        [Route("/Dashboard/HistoricalMealPlans")]
        public ActionResult HistoricalMealPlans()
        {
            var history = new List<MealPlan>();
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spHistoricalMealPlans", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter paramUserId = new SqlParameter
                {
                    ParameterName = "@UserId",
                    Value = (int)Session["id"]
                };
                cmd.Parameters.Add(paramUserId);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var m = new MealPlan();

                    m.MealPlanId = Convert.ToInt32(rdr["MealId"]);
                    m.Meals = new Meal[] {
                        Meal.ObjectMeal(rdr["Breakfast"].ToString()),
                        Meal.ObjectMeal(rdr["Lunch"].ToString()),
                        Meal.ObjectMeal(rdr["Dinner"].ToString())
                    };

                    history.Add(m);
                }
            }

            return View(history);
        }
    }
}