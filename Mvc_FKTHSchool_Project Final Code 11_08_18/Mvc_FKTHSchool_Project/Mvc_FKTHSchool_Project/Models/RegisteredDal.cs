using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class RegisteredDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public List<NotesDownloadModel> GetNotesbyStdClass(string StdId)
        {
            try
            {
                SqlCommand com_GetStudentClass = new SqlCommand("Select class from Students where StudentId=@id", con);
                com_GetStudentClass.Parameters.AddWithValue("@id", StdId);
                con.Open();
                int ofclass = Convert.ToInt32(com_GetStudentClass.ExecuteScalar());
                SqlCommand Com_GetNotesByClass = new SqlCommand("select Section,NotesDesc,NotesPath from NotesTable where OfWhichClass=@StdClass", con);
                Com_GetNotesByClass.Parameters.AddWithValue("@StdClass", ofclass);
                //con.Open();
                SqlDataReader dr = Com_GetNotesByClass.ExecuteReader();
                List<NotesDownloadModel> obj = new List<NotesDownloadModel>();
                while (dr.Read())
                {

                    NotesDownloadModel t = new NotesDownloadModel();
                    //t.NotesPath = dr.GetString(0);
                    t.Section = dr.GetString(0);
                    t.NotesDesc = dr.GetString(1);
                    t.NotesPath = dr.GetString(2);
                    t.Class = ofclass;
                    obj.Add(t);

                }

                con.Close();
                return obj;
            }
            finally
            {
                con.Close();
            }

        }

        public string GetRoutine(String StdId, String rfs)
        {
            try
            { 
            SqlCommand com_GetStudentClass = new SqlCommand("Select class from Students where StudentId=@id", con);
            com_GetStudentClass.Parameters.AddWithValue("@id", StdId);
            con.Open();
            int rfc = Convert.ToInt32(com_GetStudentClass.ExecuteScalar());
            SqlCommand com_Getroutine = new SqlCommand("select RoutinesPath from RoutinesTable where ForWhichClass=@Class and ForWhichSection=@section",con);
            com_Getroutine.Parameters.AddWithValue("@Class",rfc);
            com_Getroutine.Parameters.AddWithValue("@section",rfs);
            
            Object n =com_Getroutine.ExecuteScalar();

            if(n!=null)
            {
                String RPath = n.ToString();
                con.Close();
                return RPath;
            }
            con.Close();
            return "fail";
            }
            finally
            {
                con.Close();
            }
        }





        /////////////////////////souvik 13/11 /////////////////////////////////
        public bool sendfeedback(FeedbackModel model)
        {
            try
            {

                SqlCommand com_feedback = new SqlCommand("insert feedback values(@sid,@name,@msg)", con);
                com_feedback.Parameters.AddWithValue("@name", model.name);
                com_feedback.Parameters.AddWithValue("@sid", model.SenderID);
                com_feedback.Parameters.AddWithValue("@msg", model.feedbackMessage);


                con.Open();
                int count = com_feedback.ExecuteNonQuery();
                //con.Close();
                if (count == 1)
                {
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

        public StudentModel GetProfile(string StdId)
        {
            try
            { 
            SqlCommand com_GetStudentdtl = new SqlCommand("Select * from Students where StudentId=@id", con);
            com_GetStudentdtl.Parameters.AddWithValue("@id", StdId);
            con.Open();
            SqlDataReader dr = com_GetStudentdtl.ExecuteReader();
            StudentModel sm = new StudentModel();
            if (dr.Read())
            {
                sm.name = dr.GetString(2);
                sm.class_name = dr.GetString(11); 
            }

            return sm;
            }
            finally
            {
                con.Close();
            }
        }

    }
}