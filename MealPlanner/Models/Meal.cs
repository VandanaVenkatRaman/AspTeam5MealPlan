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
        [JsonProperty("imageUrls")]
        public string[] ImageUrls { get; set; }

        public static string StringMeal(Meal m)
        {
            return m.Id + "," + m.ReadyInMinutes + "," + m.Servings + "," + m.Title;
        }

        public static Meal ObjectMeal(string m)
        {
            List<string> temp = m.Split(',').ToList<string>();

            return new Meal
            {
                Id = int.Parse(temp.ElementAt(0)),
                ReadyInMinutes = int.Parse(temp.ElementAt(1)),
                Servings = int.Parse(temp.ElementAt(2)),
                Title = temp.ElementAt(3)
            };
        }
    }
}