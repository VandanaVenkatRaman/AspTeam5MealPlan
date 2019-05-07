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

            string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
                
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
                  
                       user.UserId = id;
                         user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Email = rdr["Email"].ToString();
                        user.Password = rdr["Password"].ToString();


                    }
                }
                return user;
            }

        


       

        // insert user into database in regirstration

        public void AddUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddNewUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = user.FirstName;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = user.LastName;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = user.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = user.Password;
                cmd.Parameters.Add(paramPassword);




                con.Open();

                cmd.ExecuteNonQuery();


            }

        }




        //update user info
        public void SaveUser(User user)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = user.UserId;
                cmd.Parameters.Add(paramUserID);

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = user.FirstName;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = user.LastName;
                cmd.Parameters.Add(paramLastName);

                SqlParameter paramEmail = new SqlParameter();
                paramEmail.ParameterName = "@Email";
                paramEmail.Value = user.Email;
                cmd.Parameters.Add(paramEmail);

                SqlParameter paramPassword = new SqlParameter();
                paramPassword.ParameterName = "@Password";
                paramPassword.Value = user.Password;
                cmd.Parameters.Add(paramPassword);




                con.Open();

                cmd.ExecuteNonQuery();


            }

        }

        //DELETE USER
        public void DeleteUser(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
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


        public int GetUserId(RegisterPageModel registerPageModel)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
            int UserID = 1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserIdOnName", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramFirstName = new SqlParameter();
                paramFirstName.ParameterName = "@FirstName";
                paramFirstName.Value = registerPageModel.User.FirstName;
                cmd.Parameters.Add(paramFirstName);

                SqlParameter paramLastName = new SqlParameter();
                paramLastName.ParameterName = "@LastName";
                paramLastName.Value = registerPageModel.User.LastName;
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