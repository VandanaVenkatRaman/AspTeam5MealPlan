﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MealPlanner.Models
{
    [Serializable]
    public class MealPlan
    {
        public int MealPlanId { get; set; }
        [JsonProperty("meals")]
        public Meal[] Meals { get; set; }
        [JsonProperty("nutrients")]
        public Object Nutrients { get; set; }
    }
}