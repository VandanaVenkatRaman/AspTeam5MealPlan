using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using WebGrease.Css.Ast;

namespace MealPlanner.Models
{
    public static class Database
    {
        public static Dictionary<string, double> ActivityEnum = new Dictionary<string, double>()
        {
            {"Sedentary", 1.2},
            {"Lightly Active", 1.375},
            {"Moderately Active", 1.55},
            {"Very Active", 1.725},
            {"Extremely Active", 1.9}
        };

        public static bool AddUserToDatabase(User u)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.AddNewUser", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("FirstName", u.FirstName);
                    com.Parameters.AddWithValue("LastName", u.LastName);
                    com.Parameters.AddWithValue("Email", u.Email);
                    com.Parameters.AddWithValue("Password", u.Password);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            int userId = reader.GetInt32(0);
                            u.UserId = userId;
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private static float GetTargetCalories(User u)
        {
            var height = (u.BodyStats.HeightFeet * 12) + u.BodyStats.HeightInches;
            var weight = u.BodyStats.Weight;
            var gender = u.BodyStats.Gender;
            var age = u.BodyStats.Age;

            //if gender == true, male calculation : false, female calculation
            var bmr = gender ? 66 + (6.3F * weight) + (12.9F * height) - (6.8 * age) : 655 + (4.3F * weight) + (4.7F * height) - (4.7 * age);

            return (float) ((u.BodyStats.LoseOrMaintainWeight)
                ? (bmr * ActivityEnum[u.BodyStats.ActivityLevel])
                : (bmr * ActivityEnum[u.BodyStats.ActivityLevel] - 500));
        }

        public static bool AddBodyStats(User u)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.AddUserBodyStats", con))
                {
                    double weight = convertWeight(u.BodyStats.Weight);
                    double height = convertHeight(u.BodyStats.HeightFeet,u.BodyStats.HeightInches );
                    double BMI = BMICalculation(weight, height);
                    

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("UserId", u.UserId);
                    com.Parameters.AddWithValue("Height", (float)height);
                    com.Parameters.AddWithValue("Weight", (float)weight);
                    com.Parameters.AddWithValue("TargetWeight", (float)u.BodyStats.WeightGoal);
                    //TODO: Fetch targetCalories from API
                    com.Parameters.AddWithValue("TargetCalories", GetTargetCalories(u));
                    com.Parameters.AddWithValue("TargetDays", (float)u.BodyStats.DaysToGoal);
                    com.Parameters.AddWithValue("BMI", (float)BMI);
                    com.Parameters.AddWithValue("ActivityLevel", u.BodyStats.ActivityLevel);
                    com.Parameters.AddWithValue("Age", u.BodyStats.Age);
                    com.Parameters.AddWithValue("Gender", u.BodyStats.Gender);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        if (reader.HasRows && reader.Read())
                        {
                            int bodyStatId = reader.GetInt32(0);
                            u.BodyStats.Id = bodyStatId;
                            return true;
                        }
                    }
                }
                return false;
            }
        }
        public static bool ValidateUser(string userName, string Password)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("dbo.ValidateUser", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("InputEmail", userName);
                    com.Parameters.AddWithValue("InputPassword", Password);
                    using (SqlDataReader reader = com.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
        public static double convertWeight(int pounds)
        {
            double kg;
            kg = pounds * 0.45;
            return kg;
        }
        public static double convertHeight(int feets, int inches)
        {
            double cm;
            cm = ((feets * 12) + inches) * 2.5;
            return cm;
        }
        public static double BMICalculation(double kg, double cm)
        {
            double BMI;
            BMI = kg / Math.Pow(cm / 100.0, 2);
            return BMI;

        }
    }
}