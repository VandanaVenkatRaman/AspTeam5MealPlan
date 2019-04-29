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
        [Display(Name = "HeightFeet")]
        public double HeightFeet { get; set; }

        [Required]
        [Display(Name = "HeightInches")]
        public double HeightInches { get; set; }


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
        public MealPlanner.Models.Enums.ActivityLevel ActivityLevel { get; set; }



        [Required]
        [Display(Name = "Age")]
        public int Age { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public MealPlanner.Models.Enums.Gender Gender  { get; set; }

        [Required]
        [Display(Name = "AddDate")]
        public DateTime AddDate { get; set; }




    }
}