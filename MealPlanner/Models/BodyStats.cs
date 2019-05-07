using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public class BodyStats
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("loseOrMaintainWeight")]
        public bool LoseOrMaintainWeight { get; set; }
        [JsonProperty("heightFeet")]
        public int HeightFeet { get; set; }
        [JsonProperty("heightInches")]
        public int HeightInches { get; set; }
        [JsonProperty("weight")]
        public int Weight { get; set; }
        [JsonProperty("activityLevel")]
        public string ActivityLevel { get; set; }
        [JsonProperty("weightGoal")]
        public int WeightGoal { get; set; }
        [JsonProperty("daysToGoal")]
        public int DaysToGoal { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
        [JsonProperty("gender")]
        public bool Gender { get; set; }
    }
}