using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public class User
    {
        //public int UserID { get; set; }
        //[Required]
        //public string FirstName { get; set; }
        //[Required]
        //public string LastName { get; set; }

        //[Required]
        //[EmailAddress]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
       // public string Password { get; set; }


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
        public BodyStats SingleBodyStats { get; set; }

        public List<BodyStats> BodyStats { get; set; }


    }
}