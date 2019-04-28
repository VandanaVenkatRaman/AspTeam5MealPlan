using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    [Serializable]
    public class Meal
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("readyInMinutes")]
        public int ReadyInMinutes { get; set; }
        [JsonProperty("servings")]
        public int Servings { get; set; }
        [JsonProperty("image")]
        public string Image { get; set; }
    }
}