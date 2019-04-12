using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        public void Register()
        {

        }

        [HttpPost]
        public void SignIn()
        {

        }

        public void SignOut()
        {

        }
    }
}