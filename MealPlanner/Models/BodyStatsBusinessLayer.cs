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
        PhysicalCalculation physicalCalculation = new PhysicalCalculation();

        // retrieve data to outpur to view
        public List<BodyStats> GetBodyStats(int id)
        {
            
                string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
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
                    // how to convert object to enum type: PetType pet = (PetType)Enum.Parse(typeof(PetType), value);

                    BodyStats body = new BodyStats();
                         
                        body.BodyStatId = Convert.ToInt32(rdr["BodyStatId"]);
                         body.UserId = id;



                      body.HeightFeet = physicalCalculation.CmConvertFeet(Convert.ToDouble(rdr["Height"]));


                    body.HeightInches = physicalCalculation.CmConvertIn(Convert.ToDouble(rdr["Height"]));

                   
                    body.Weight = physicalCalculation.ConvertToLb( Convert.ToDouble(rdr["Weight"]));
                    body.TargetWeight = physicalCalculation.ConvertToLb(Convert.ToDouble(rdr["TargetWeight"]));
                    
                          body.TargetCalories = Convert.ToDouble(rdr["TargetCalories"]);
                        body.TargetDays = Convert.ToInt32(rdr["TargetDays"]);
                        body.BMI = Convert.ToDouble(rdr["BMI"]);

                    body.ActivityLevel=(Enums.ActivityLevel)Enum.Parse(typeof(Enums.ActivityLevel), rdr["ActivityLevel"].ToString());
                   // body.ActivityLevel = rdr["ActivityLevel"].ToString();
                     

                        body.Age = Convert.ToInt32(rdr["Age"]);
                    body.Gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), rdr["Gender"].ToString());
                   // body.Gender = (bool)(rdr["Gender"]);
                    body.AddDate = Convert.ToDateTime(rdr["AddDate"]);
                    


                    bodyStatss.Add(body);
                    }
                }

                return bodyStatss;
            }


      

     //   add new user's bodyStats
        public void AddBodyStats(BodyStats  bodyStats)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddNewBodyStats", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserID = new SqlParameter();
                paramUserID.ParameterName = "@UserID";
                paramUserID.Value = bodyStats.UserId;
                cmd.Parameters.Add(paramUserID);

                SqlParameter paramHeight = new SqlParameter();
                paramHeight.ParameterName = "@Height";    
                double height = physicalCalculation.ConvertHeight(bodyStats.HeightFeet, bodyStats.HeightInches);
                paramHeight.Value = height;
                cmd.Parameters.Add(paramHeight);

                SqlParameter paramWeight = new SqlParameter();
                paramWeight.ParameterName = "@Weight";
                double weightCurrent = physicalCalculation.ConvertWeight(bodyStats.Weight);
                paramWeight.Value = weightCurrent;
                cmd.Parameters.Add(paramWeight);

                SqlParameter paramTargetWeight = new SqlParameter();
                paramTargetWeight.ParameterName = "@TargetWeight";
                double weightTarget = physicalCalculation.ConvertWeight(bodyStats.TargetWeight);
                paramTargetWeight.Value = weightTarget;
                cmd.Parameters.Add(paramTargetWeight);

                SqlParameter paramTargetDays = new SqlParameter();
                paramTargetDays.ParameterName = "@TargetDays";
                paramTargetDays.Value = bodyStats.TargetDays;
                cmd.Parameters.Add(paramTargetDays);

                SqlParameter paramActivityLevel = new SqlParameter();
                paramActivityLevel.ParameterName = "@ActivityLevel";
                paramActivityLevel.Value = bodyStats.ActivityLevel;
                cmd.Parameters.Add(paramActivityLevel);

                SqlParameter paramAge = new SqlParameter();
                paramAge.ParameterName = "@Age";
                paramAge.Value = bodyStats.Age;
                cmd.Parameters.Add(paramAge);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@Gender";
                paramGender.Value = bodyStats.Gender;
                cmd.Parameters.Add(paramGender);

                SqlParameter paramBMI = new SqlParameter();
                paramBMI.ParameterName = "@BMI";
                paramBMI.Value = physicalCalculation.BMIcalculation(weightCurrent,height);
                cmd.Parameters.Add(paramBMI);

                SqlParameter paramTargetCalories = new SqlParameter();
                paramTargetCalories.ParameterName = "@TargetCalories";
                 paramTargetCalories.Value = physicalCalculation.GetCalories(height, weightTarget, bodyStats.Age, bodyStats.Gender, bodyStats.ActivityLevel);
             
                 cmd.Parameters.Add(paramTargetCalories);



                con.Open();

                cmd.ExecuteNonQuery();


            }

        }


        // edit user's bodyStats

        public void SaveBodyStats(BodyStats bodyStats)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spSaveBodyStats", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBodyStatId = new SqlParameter();
                paramBodyStatId.ParameterName = "@BodyStatId";
                paramBodyStatId.Value = bodyStats.BodyStatId;
                cmd.Parameters.Add(paramBodyStatId);

                SqlParameter paramHeight = new SqlParameter();
                paramHeight.ParameterName = "@Height";
                double height = physicalCalculation.ConvertHeight(bodyStats.HeightFeet, bodyStats.HeightInches);
                paramHeight.Value = height;
                cmd.Parameters.Add(paramHeight);

                SqlParameter paramWeight = new SqlParameter();
                paramWeight.ParameterName = "@Weight";
                double weightCurrent = physicalCalculation.ConvertWeight(bodyStats.Weight);
                paramWeight.Value = weightCurrent;
                cmd.Parameters.Add(paramWeight);

                SqlParameter paramTargetWeight = new SqlParameter();
                paramTargetWeight.ParameterName = "@TargetWeight";
                double weightTarget = physicalCalculation.ConvertWeight(bodyStats.Weight);
                paramTargetWeight.Value = weightTarget;
                cmd.Parameters.Add(paramTargetWeight);

                SqlParameter paramTargetDays = new SqlParameter();
                paramTargetDays.ParameterName = "@TargetDays";
                paramTargetDays.Value = bodyStats.TargetDays;
                cmd.Parameters.Add(paramTargetDays);

                SqlParameter paramActivityLevel = new SqlParameter();
                paramActivityLevel.ParameterName = "@ActivityLevel";
                paramActivityLevel.Value = bodyStats.ActivityLevel;
                cmd.Parameters.Add(paramActivityLevel);

                SqlParameter paramAge = new SqlParameter();
                paramAge.ParameterName = "@Age";
                paramAge.Value = bodyStats.Age;
                cmd.Parameters.Add(paramAge);

                SqlParameter paramGender = new SqlParameter();
                paramGender.ParameterName = "@Gender";
                paramGender.Value = bodyStats.Gender;
                cmd.Parameters.Add(paramGender);

                SqlParameter paramBMI = new SqlParameter();
                paramBMI.ParameterName = "@BMI";
                paramBMI.Value = physicalCalculation.BMIcalculation(weightCurrent, height);
                cmd.Parameters.Add(paramBMI);

                SqlParameter paramTargetCalories = new SqlParameter();
                paramTargetCalories.ParameterName = "@TargetCalories";
                paramTargetCalories.Value = physicalCalculation.GetCalories(height, weightTarget, bodyStats.Age, bodyStats.Gender, bodyStats.ActivityLevel);
                cmd.Parameters.Add(paramTargetCalories);


                con.Open();

                cmd.ExecuteNonQuery();


            }

        }


        //DELETE one bodyStatus
        public void DeleteBodyStats(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteBodyStats", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBodyStatId = new SqlParameter();
                paramBodyStatId.ParameterName = "@BodyStatId";
                paramBodyStatId.Value = id;
                cmd.Parameters.Add(paramBodyStatId);

                con.Open();
                cmd.ExecuteNonQuery();


            }
        }

        public int GetUserId(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MealPlanDatabaseConnection"].ConnectionString;
            int UserID = 1;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetUserId", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramBodyStatId = new SqlParameter();
                paramBodyStatId.ParameterName = "@BodyStatId";
                paramBodyStatId.Value = id;
                cmd.Parameters.Add(paramBodyStatId);

                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserID = Convert.ToInt32(rdr["UserId"]);
                }


            }

            return UserID;
        }


    }
}