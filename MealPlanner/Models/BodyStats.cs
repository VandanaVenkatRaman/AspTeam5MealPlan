using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public class BodyStats
    {

        
        public int BodyStatId { get; set; }

        [Display(Name = "UserId")]
        public int UserId { get; set; }


        [Required]
        [Display(Name = "Height")]
        public double Height { get; set; }


        [Required]
        [Display(Name = "Weight")]
        public double Weight { get; set; }

        [Required]
        [Display(Name = "TargetWeight")]
        public double TargetWeight { get; set; }


        
        [Display(Name = "TargetCalories")]
        public double TargetCalories { get; set; }

        [Required]
        [Display(Name = "TargetDays")]
        public int TargetDays { get; set; }



        [Display(Name = "BMI")]
        public double BMI { get; set; }



        [Required]
        [Display(Name = "ActivityLevel")]
        public string ActivityLevel { get; set; }



        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public bool Gender { get; set; }




    }
}