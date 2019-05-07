using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MealPlanner.Models.Enums;

namespace MealPlanner.Models
{
    public class PhysicalCalculation
    {
        //convert to cm from feet and inch
        public double ConvertWeight(double pounds)
        {
            double kg;
            kg = pounds * 0.45;
            return kg;
        }

        //convert to lg from lb
        public double ConvertHeight(double feets, double inches)
        {
            double cm;
            cm = ((feets * 12) + inches) * 2.5;
            return cm;
        }

        //convert cm to heet and inch 
//   
            public double CmConvertFeet(double cm)
        {
            int feet = 0;
            int inches = (int)(Math.Round(cm / 2.54));

            feet = inches / 12;
            return (double)feet;
        }



        public double CmConvertIn(double cm)
        {
            double In = 0;
            int inches = (int)(Math.Round(cm / 2.54));

            In = inches % 12;
            return In;
        }


        //convert lg to lb
        public double ConvertToLb(double kg)
        {
            double lb;
           
            lb = kg / 0.45;
            return lb;
        }

        public double BMIcalculation(double kg, double cm)
        {
            double BMI;


            BMI = kg / Math.Pow(cm / 100.0, 2);
            return BMI;


        }
        //public static Dictionary<string, double> ActivityEnum = new Dictionary<string, double>()
        //{
        //    {"Sedentary", 1.2},
        //    {"Lightly Active", 1.375},
        //    {"Moderately Active", 1.55},
        //    {"Very Active", 1.725},
        //    {"Extremely Active", 1.9}
        //};
        //private static float GetTargetCalories(User u)
        //{
        //    var height = (u.SingleBodyStats.HeightFeet * 12) + u.SingleBodyStats.HeightInches;
        //    var weight = u.SingleBodyStats.Weight;
        //    var gender = u.SingleBodyStats.Gender;
        //    var age = u.SingleBodyStats.Age;

        //    //if gender == true, male calculation : false, female calculation
        //    var bmr = gender ? 66 + (6.3F * weight) + (12.9F * height) - (6.8 * age) : 655 + (4.3F * weight) + (4.7F * height) - (4.7 * age);

        //    return (float)((u.SingleBodyStats.LoseOrMaintainWeight)
        //        ? (bmr * ActivityEnum[u.SingleBodyStats.ActivityLevel])
        //        : (bmr * ActivityEnum[u.SingleBodyStats.ActivityLevel] - 500));
        //}
        
        public int GetBmrmult(Enums.ActivityLevel ExerciseLevel)
        {


            int bmrmult = 0;

            if ((int)ExerciseLevel != -1)
            {
                //string exercise;

                //exercise = ExerciseLevel.ToString();
                switch (ExerciseLevel)

                {
                    case Enums.ActivityLevel.Sedentary:
                        bmrmult = (int)1.2m;
                        break;
                    case Enums.ActivityLevel.LightlyActive:
                        bmrmult = (int)1.375m;
                        break;
                    case Enums.ActivityLevel.ModeratelyActive:
                        bmrmult = (int)1.55m;
                        break;
                    case Enums.ActivityLevel.VeryActive:
                        bmrmult = (int)1.725m;
                        break;
                    case Enums.ActivityLevel.ExtraActive:
                        bmrmult = (int)1.9m;
                        break;
                }


            }

            return bmrmult;

        }

        public int GetCalories(double height, double weight, int age, Enums.Gender gender, Enums.ActivityLevel exerciseLevel)
        {

            double BMR = 0;

            int Calories;
            //select gender
            //if (genderList.SelectedIndex != -1)
            if ((int)gender != -1)
            {
                //string gender = g.ToString();
                switch (gender)
                {
                    case Enums.Gender.Male:
                        //perform calculation
                        BMR = (weight * 10 + height * 6.25 - age * 5 - 5);

                        break;

                    case Enums.Gender.Female:
                        BMR = weight * 10 + height * 6.25 - age * 5 - 161;
                        break;

                }
            }


            Calories = Convert.ToInt32(BMR * GetBmrmult(exerciseLevel));
            return Calories;

        }
        

    }
}