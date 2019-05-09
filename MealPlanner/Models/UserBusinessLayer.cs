using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MealPlanner.Models
{
    public class UserBusinessLayer
    {



        public User GetUser(int id)
        {
            User user = new User();

            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUser", con);
                cmd.CommandType = CommandType.StoredProcedure;


                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = id;
                cmd.Parameters.Add(paramUserID);

                con.Open();


                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    user.userid = id;
                    user.fname = rdr["FirstName"].ToString();
                    user.lname = rdr["LastName"].ToString();
                    user.email = rdr["Email"].ToString();
                    user.password = rdr["Password"].ToString();


                }
            }
            return user;
        }






        // insert user into database in regirstration

        public void AddUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddNewUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = user.fname;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = user.lname;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = user.email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = user.password;
                cmd.Parameters.Add(paramPassword);




                con.Open();

                cmd.ExecuteNonQuery();


            }

        }




        //update user info
        public void SaveUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = user.userid;
                cmd.Parameters.Add(paramUserID);

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = user.fname;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = user.lname;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = user.email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = user.password;
                cmd.Parameters.Add(paramPassword);




                con.Open();

                cmd.ExecuteNonQuery();


            }

        }

        //DELETE USER
        public void DeleteUser(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = id;
                cmd.Parameters.Add(paramUserID);

                con.Open();
                cmd.ExecuteNonQuery();


            }
        }


        public int GetUserId(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            int UserID = 1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserIdOnName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = user.fname;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = user.lname;
                cmd.Parameters.Add(paramLastName);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserID = Convert.ToInt32(rdr["UserID"]);
                }


            }

            return UserID;
        }





    }
}