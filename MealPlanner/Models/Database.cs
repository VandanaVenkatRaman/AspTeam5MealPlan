using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public static class Database
    {
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
                    com.Parameters.AddWithValue("TargetCalories", 123);
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