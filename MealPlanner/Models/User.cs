using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
     
        public string Email { get; set; }
        public string Password { get; set; }

        // public List<Employee> Employee { get; set; }
    }
}