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
            {"Moderate", 1.55},
            {"VeryActive", 1.725},
            {"ExtremelyActive", 1.9}
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
                    com.Parameters.AddWithValue("Age", u.Age);
                    com.Parameters.AddWithValue("Gender", u.Gender);
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
            var gender = u.Gender;
            var age = u.Age;

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
                    

                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("UserId", u.UserId);
                    com.Parameters.AddWithValue("Height", (float)u.BodyStats.HeightFeet);
                    com.Parameters.AddWithValue("Weight",(float)u.BodyStats.Weight);
                    com.Parameters.AddWithValue("TargetWeight", (float)u.BodyStats.WeightGoal);
                    //TODO: Fetch targetCalories from API
                    com.Parameters.AddWithValue("TargetCalories", GetTargetCalories(u));
                    com.Parameters.AddWithValue("TargetDays", (float)u.BodyStats.DaysToGoal);
                    com.Parameters.AddWithValue("BMI", u.BodyStats.WeightGoal * 1.0 / u.BodyStats.HeightFeet);
                    com.Parameters.AddWithValue("ActivityLevel", u.BodyStats.ActivityLevel);
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
    }
}