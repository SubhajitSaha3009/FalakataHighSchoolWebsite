using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class PublicViewDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
        public List<NoticeDownloadModel> getNotice()
        {
            //try
            //{ 
            ///////////////////////////////////////Updating notice status//////////////////////////////

            SqlCommand com_updateNoticeStatus = new SqlCommand("update notices set Status='old' WHERE NoticeDate < DATEADD(day, -10, GETDATE())", con);
            con.Open();
            com_updateNoticeStatus.ExecuteNonQuery();
            con.Close();

            //////////////////////////////////////Getting notice details////////////////////////////////

            SqlCommand com_getNotice = new SqlCommand("select top 10 NoticeTitle,NoticePath,Status from Notices order by NoticeDate desc", con);
            con.Open();
            SqlDataReader dr = com_getNotice.ExecuteReader();
            List<NoticeDownloadModel> getList = new List<NoticeDownloadModel>();
            while (dr.Read())
            {

                NoticeDownloadModel n_model = new NoticeDownloadModel();
                n_model.title = dr.GetString(0);
                n_model.href = dr.GetString(1);
                n_model.status = dr.GetString(2);
                getList.Add(n_model);

            }

           // con.Close();
            return getList;
            //}
            //finally
            //{
            //    con.Close();
            //}
        }

        public List<ViewGalleryModel> getAllPhotos()
        {
            try
            {
                SqlCommand com_getAllPhotos = new SqlCommand("select * from Gallery order by imageID desc", con);
                con.Open();

                SqlDataReader dr = com_getAllPhotos.ExecuteReader();
                List<ViewGalleryModel> obj = new List<ViewGalleryModel>();

                while (dr.Read())
                {
                    ViewGalleryModel vgm = new ViewGalleryModel();
                    vgm.title = dr.GetString(1);
                    vgm.imagePath = dr.GetString(2);
                    vgm.imageDescription = dr.GetString(3);
                    obj.Add(vgm);
                }
                return obj;
            }
            finally
            {
                con.Close();
            }
        }




        public List<ViewToppersListModel> TopperPicLoad()
        {
            try
            {
                SqlCommand com_getAllTopper = new SqlCommand("select * from Toppers_list order by class", con);
                con.Open();
                SqlDataReader dr = com_getAllTopper.ExecuteReader();

                List<ViewToppersListModel> obj = new List<ViewToppersListModel>();
                while (dr.Read())
                {
                    ViewToppersListModel vtl = new ViewToppersListModel();
                    vtl.OfClass = dr.GetInt32(0);
                    vtl.FirstName = dr.GetString(1);
                    vtl.SecondName = dr.GetString(2);
                    vtl.ThirdName = dr.GetString(3);
                    vtl.F_path = dr.GetString(4);
                    vtl.S_path = dr.GetString(5);
                    vtl.T_path = dr.GetString(6);
                    obj.Add(vtl);

                }
                return obj;
            }
            finally
            {
                con.Close();
            }
        }


        public List<GetAllTeacherModel> getAllTeachers()
        {
            try
            {
                SqlCommand com_getAllTeacher = new SqlCommand("select TeacherName,TeacherDesignation,TeacherQualification,SubjectSpecialisation,imageAddress,TeacherGender,teacherStatus from Teachers group by SubjectSpecialisation,TeacherQualification, teachergender,TeacherName,TeacherDesignation,imageAddress,teacherStatus", con);
            con.Open();
            SqlDataReader dr = com_getAllTeacher.ExecuteReader();
            List<GetAllTeacherModel> Lgt = new List<GetAllTeacherModel>();
            while (dr.Read())
            {
                GetAllTeacherModel obj = new GetAllTeacherModel();
                obj.teacherName = dr.GetString(0);
                obj.teacherDesignation = dr.GetString(1);
                obj.Qualification = dr.GetString(2);
                obj.subjectSpecilization = dr.GetString(3);
                obj.imageAddress = dr.GetString(4);
                obj.teacherGender = dr.GetString(5);
                obj.teacherStatus = dr.GetString(6);
                Lgt.Add(obj);
            }

            //con.Close();
            return Lgt;
            }
            finally
            {
                con.Close();
            }

        }


        //////////////////////////////////// view news ///////////////////////////////////////////////


        public List<NewsModel> getAllNews()
        {
            try
            { 
            SqlCommand com_getNews = new SqlCommand("select newsTitle, newsDescription, newsDate, newsImageAddress from news order by newsDate desc", con);
            con.Open();
            SqlDataReader dr = com_getNews.ExecuteReader();
            List<NewsModel> list_news = new List<NewsModel>();
            while (dr.Read())
            {
                NewsModel n_model = new NewsModel();
                n_model.newsTitle = dr.GetString(0);
                n_model.newsDescription = dr.GetString(1);
                n_model.newsDate = dr.GetDateTime(2).ToShortDateString();
                n_model.newsImageAddress = dr.GetString(3);
                list_news.Add(n_model);
            }


            //con.Close();
            return list_news;
            }
            finally
            {
                con.Close();
            }
        }

        /////////////////////////////////////subha 12/11/2017////////////////////
        public List<AlumniModel> ViewAllAlumni()
        {
            try
            { 
            SqlCommand com_getAllAlumni = new SqlCommand("Select * from Alumni order by AlumniYear desc", con);
            con.Open();
            SqlDataReader dr = com_getAllAlumni.ExecuteReader();
            List<AlumniModel> list_getAllAlumni = new List<AlumniModel>();
            while (dr.Read())
            {
                AlumniModel a_model = new AlumniModel();
                a_model.alumniID = dr.GetInt32(0);
                a_model.name = dr.GetString(1);
                a_model.allumniClass = dr.GetString(2);
                a_model.year = dr.GetDateTime(3).Year.ToString();
                list_getAllAlumni.Add(a_model);
            }
           // con.Close();
            return list_getAllAlumni;
            }
            finally
            {
                con.Close();
            }
        }  

    }
}