using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using unirest_net.http;

namespace MealPlanner.Models
{
    public class MealPlan
    {
        public List<Meal> Meals { get; set; }

        public MealPlan(string meal)
        {
            var jsonStr = new JavaScriptSerializer();

            Meals = jsonStr.Deserialize<List<Meal>>(meal);
        }
    }
}