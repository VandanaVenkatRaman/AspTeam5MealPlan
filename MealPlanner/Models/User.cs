using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    [Serializable]
    public class User
    {
        [JsonProperty("fname")]
        public string FirstName { get; set; }
        [JsonProperty("lname")]
        public string LastName { get; set; }
        [JsonProperty("userId")]
        public int UserId { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("confirmPassword")]
        public string ConfirmPassword { get; set; }
        [JsonProperty("bodyStats")]
        public BodyStats BodyStats { get; set; }
      
    }
}