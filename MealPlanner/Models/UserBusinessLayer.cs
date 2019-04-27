﻿using System;
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
        // retrieve data to outpur to view
        public IEnumerable<User> Users
        {
            get
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MealPlannerContext"].ConnectionString;
                List<User> users = new List<User>();

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spGetAllUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();

                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.UserID = Convert.ToInt32(rdr["UserID"]);
                        user.FirstName = rdr["FirstName"].ToString();
                        user.LastName = rdr["LastName"].ToString();
                        user.Email = rdr["Email"].ToString();
                        user.Password = rdr["Password"].ToString();

                       

                        users.Add(user);
                    }
                }

                return users;
            }

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
                paramUserID.Value = user.UserID;
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


        




    }
}