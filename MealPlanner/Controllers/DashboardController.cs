using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MealPlanner.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PhysicalProfile()
        {
            return View("PhysicalProfile");
        }
        public ActionResult UserDashboard()
        {
            if (HttpContext.Session["UserId"] != null)
            {
                string userid = HttpContext.Session["UserId"].ToString();
                return View("UserDashboard");
            }
            else
            {
                return View("Error");
            }
        }
    }
}