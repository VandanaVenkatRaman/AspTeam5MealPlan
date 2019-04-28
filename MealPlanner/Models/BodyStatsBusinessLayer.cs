using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MealPlanner.Models
{
    public class BodyStatsBusinessLayer
    {
        // retrieve data to outpur to view
        public List<BodyStats> GetBodyStats(int id)
        {
            
                string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
                List<BodyStats> bodyStatss = new List<BodyStats>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllBodyStats", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUserID = new SqlParameter();
                    paramUserID.ParameterName = "@UserId";
                    paramUserID.Value = id;
                    cmd.Parameters.Add(paramUserID);
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {


                        BodyStats body = new BodyStats();
                         body.BodyStatId = id;
                        body.UserId = Convert.ToInt32(rdr["UserId"]);
                        body.Height = Convert.ToDouble(rdr["Height"]);
                        body.TargetWeight = Convert.ToDouble(rdr["TargetWeight"]);
                        body.TargetCalories = Convert.ToDouble(rdr["TargetCalories"]);
                        body.TargetDays = Convert.ToInt32(rdr["TargetDays"]);
                        body.BMI = Convert.ToDouble(rdr["BMI"]);
                        body.ActivityLevel = rdr["ActivityLevel"].ToString();
                        body.Age = Convert.ToInt32(rdr["Age"]);
                        body.Gender = Convert.ToBoolean(rdr["Gender"]);



                        bodyStatss.Add(body);
                    }
                }

                return bodyStatss;
            }

        



        //add user's bodyStats
        //public void AddBodyStats(User user)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
        //    using (SqlConnection con = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("spAddNewBodyStats", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //         SqlParameter paramUserID = new SqlParameter();
        //        paramUserID.ParameterName = "@UserID";
        //        paramUserID.Value = user.UserID;
        //        cmd.Parameters.Add(paramUserID);

        //        SqlParameter paramHeight = new SqlParameter();
        //        paramHeight.ParameterName = "@Height";
        //        paramHeight.Value = user.BodyStats.Single(;
        //        cmd.Parameters.Add(paramHeight);

        //        SqlParameter paramFirstName = new SqlParameter();
        //        paramFirstName.ParameterName = "@FirstName";
        //        paramFirstName.Value = user.FirstName;
        //        cmd.Parameters.Add(paramFirstName);

        //        SqlParameter paramLastName = new SqlParameter();
        //        paramLastName.ParameterName = "@LastName";
        //        paramLastName.Value = user.LastName;
        //        cmd.Parameters.Add(paramLastName);

        //        SqlParameter paramEmail = new SqlParameter();
        //        paramEmail.ParameterName = "@Email";
        //        paramEmail.Value = user.Email;
        //        cmd.Parameters.Add(paramEmail);

        //        SqlParameter paramPassword = new SqlParameter();
        //        paramPassword.ParameterName = "@Password";
        //        paramPassword.Value = user.Password;
        //        cmd.Parameters.Add(paramPassword);




        //        con.Open();

        //        cmd.ExecuteNonQuery();


        //    }

        //}








    }
}