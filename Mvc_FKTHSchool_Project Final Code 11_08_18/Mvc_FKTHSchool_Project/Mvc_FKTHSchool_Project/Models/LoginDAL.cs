﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace Mvc_FKTHSchool_Project.Models
{
    public class LoginDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);

       //B LoginTDAL dal = new LoginTDAL();
        public CheckPasswordStatusEnum loginTeacher(LogInModel model)
        {
            try
            {


                SqlCommand com_getTeacherStatus = new SqlCommand("select count(*) from Teachers where teacherid=@tid and teacherStatus='Current teacher'", con);
                com_getTeacherStatus.Parameters.AddWithValue("@tid", model.LogInId);
                con.Open();
                int c = Convert.ToInt32(com_getTeacherStatus.ExecuteScalar());
                con.Close();
                if (c == 1)
                {
                    if (Membership.ValidateUser(model.LogInId.ToString(), model.Password))
                    {
                        if (checkTeacherStatus(model))
                        {
                            //con.Close();
                            return CheckPasswordStatusEnum.Updated;
                        }
                        else
                        {
                            // con.Close();
                            return CheckPasswordStatusEnum.NewUser;
                        }
                    }
                    else
                    {
                        // con.Close();
                        return CheckPasswordStatusEnum.WrongPassword;
                    }
                }
                else
                {
                    return CheckPasswordStatusEnum.WrongPassword;
                }


            }
            finally
            {
                con.Close();
            }
          } 
        
        public bool checkTeacherStatus(LogInModel model)
        {
            try
            { 
            SqlCommand com_check = new SqlCommand("select PasswordStatus from Teachers where teacherid=@tid", con);
            com_check.Parameters.AddWithValue("@tid",model.LogInId);
            con.Open();
            string passStatus = com_check.ExecuteScalar().ToString();
            if (passStatus == "AutoGenerated")
            {
                return false;
            }
            else
            {
                return true;
            }
            }
            finally
            {
                con.Close();
            }
        }

        public bool updatePassword(int teacherid,UpdatePasswordTeacherModel c)
        {
            try
            { 
            con.Open();
            MembershipUser user = Membership.GetUser(teacherid.ToString());
            if (user.ChangePassword(c.oldPassword, c.newPassword))
            {
                user.ChangePasswordQuestionAndAnswer(c.newPassword, c.securityQuestion, c.securityAnswer);

                SqlCommand com_updatePasswordStatus = new SqlCommand("update Teachers set passwordStatus='Updated' where teacherid=@tid", con);
                com_updatePasswordStatus.Parameters.AddWithValue("@tid", teacherid);
                com_updatePasswordStatus.ExecuteNonQuery();
                //con.Close();


                return true;
            }
            else
            {
                return false;
            }
            }
            finally
            {
                con.Close();
            }
        }

        public int studentLogIn(LogInModel l)
        {
            try
            { 
            SqlCommand com_getLogInCount = new SqlCommand("select count(*) from students where StudentID=@StudentID and StudentPassword=@StudentPassword and StudentStatus='Current'", con);
            com_getLogInCount.Parameters.AddWithValue("@StudentID", l.LogInId);
            com_getLogInCount.Parameters.AddWithValue("@StudentPassword", l.Password);
            con.Open();
            int count = Convert.ToInt32(com_getLogInCount.ExecuteScalar());
            //con.Close();
            return count;
            }
            finally
            {
                con.Close();
            }
        }

       
        ////////////////////////////////////admin login////////////////////////////////
        public CheckPasswordStatusEnum loginAdmin(LoginAdminModel model)
        {
            try
            { 
            SqlCommand com_adminStatus = new SqlCommand("select count(*) from Admins where AdminID=@aid and adminStatus='Active'", con);
            com_adminStatus.Parameters.AddWithValue("@aid", model.LogInId);
            con.Open();
            int c = Convert.ToInt32(com_adminStatus.ExecuteScalar());
            con.Close();
            if (c == 1)
            {
                if (Membership.ValidateUser(model.LogInId.ToString(), model.Password))
                {
                    if (checkAdminStatus(model))
                    {
                        return CheckPasswordStatusEnum.Updated;
                    }
                    else
                    {
                        return CheckPasswordStatusEnum.NewUser;
                    }
                }
                else
                {
                    return CheckPasswordStatusEnum.WrongPassword;
                }
            }
            else
            {
                return CheckPasswordStatusEnum.WrongPassword;
            }
            }
            finally
            {
                con.Close();
            }

        }
        public bool checkAdminStatus(LoginAdminModel model)
        {
            try
            { 
            SqlCommand com_check = new SqlCommand("select PasswordStatus from Admins where adminid=@aid", con);
            com_check.Parameters.AddWithValue("@aid", model.LogInId);
            con.Open();
            string passStatus = com_check.ExecuteScalar().ToString();
            if (passStatus == "AutoGenerated")
            {
                return false;
            }
            else
            {
                return true;
            }
            }
            finally
            {
                con.Close();
            }
        }

        public bool updatePasswordAdmin(int adminid, UpdatePasswordAdminModel c)
        {
            try
            { 
            con.Open();
            MembershipUser user = Membership.GetUser(adminid.ToString());
            if (user.ChangePassword(c.oldPassword, c.newPassword))
            {
                user.ChangePasswordQuestionAndAnswer(c.newPassword, c.securityQuestion, c.securityAnswer);

                SqlCommand com_updatePasswordStatus = new SqlCommand("update Admins set passwordStatus='Updated' where adminid=@aid", con);
                com_updatePasswordStatus.Parameters.AddWithValue("@aid", adminid);
                com_updatePasswordStatus.ExecuteNonQuery();
               // con.Close();


                return true;
            }
            else
            {
                return false;
            }
            }
            finally
            {
                con.Close();
            }
        }




        /////////////////////////////souvik 10/11 //////////////////////////////
        public string forgetPassword(ForgetPasswordModel model)
        {
            try
            {
                con.Open();
                MembershipUser user = Membership.GetUser(model.loginID.ToString());

                try
                {
                    string password = user.ResetPassword(model.securityAnswer);
                    //string password = user.ResetPassword();
                    //con.Close();
                    return password;
                }
                catch
                {
                    //con.Close();
                    return null;
                }


            }
            finally
            {
                con.Close();
            }

        }
    }
}