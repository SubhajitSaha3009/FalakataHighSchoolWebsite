using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Mvc_FKTHSchool_Project.Models
{
    public class TeacherDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ToString());

        public string UploadNotes(NotesModel Note,int _id)
        {
            try
            {
                            
            Note.TeacherId = _id;
            SqlCommand Com_ADDNote = new SqlCommand("insert into NotesTable values(@TeacherId,@NotesPath,@OfWhichClass,@Section,@NotesDesc)", con);
            Com_ADDNote.Parameters.AddWithValue("@TeacherId", Note.TeacherId);
            //Com_ADDNote.Parameters.AddWithValue("@TeacherName", Note.TeacherName);
            Com_ADDNote.Parameters.AddWithValue("@NotesPath", "");
            Com_ADDNote.Parameters.AddWithValue("@OfWhichClass", Note.OfClass);
            Com_ADDNote.Parameters.AddWithValue("@Section", Note.Section);
            Com_ADDNote.Parameters.AddWithValue("@NotesDesc", Note.NoteSDesc);
            con.Open();
            Com_ADDNote.ExecuteNonQuery();

            SqlCommand com_getNotesID = new SqlCommand("select @@identity", con);
            Note.NotesID = Convert.ToInt32(com_getNotesID.ExecuteScalar());

            //For naming We have taken the number of notes uploaded and the teacher id concated
            SqlCommand com_getNoOfNotesUploaded = new SqlCommand("Select NoOfnotesUploaded from Teachers where TeacherID=@Tid", con);
            com_getNoOfNotesUploaded.Parameters.AddWithValue("@Tid", Note.TeacherId);
            int num = Convert.ToInt32(com_getNoOfNotesUploaded.ExecuteScalar());
            num++;
            SqlCommand Com_UpdateNotesCount = new SqlCommand("update Teachers set NoOfNotesUploaded=@count where TeacherID=@id", con);
            Com_UpdateNotesCount.Parameters.AddWithValue("@count", num);
            Com_UpdateNotesCount.Parameters.AddWithValue("@id",Note.TeacherId);
            Com_UpdateNotesCount.ExecuteNonQuery();
           


            string FileAddress = "/Notes/" + Note.TeacherId.ToString() + "_" + num.ToString() +"_"+ Note.NotesID + Path.GetExtension(Note.file.FileName);
            SqlCommand Com_UpdatePath = new SqlCommand("update NotesTable set NotesPath=@notespath where NotesID=@id", con);
            Com_UpdatePath.Parameters.AddWithValue("@notespath", FileAddress);
            Com_UpdatePath.Parameters.AddWithValue("@id", Note.NotesID);
            Com_UpdatePath.ExecuteNonQuery();


            //con.Close();
            return Note.TeacherId.ToString() + "_" + num.ToString() +"_"+ Note.NotesID + Path.GetExtension(Note.file.FileName);
             }

            finally
            {
                con.Close();
            }
        }

        public string UploadNotice(NoticeModel Notice,int _id)
        {
            try
            {
                
            Notice.AdminId = _id;
            SqlCommand Com_ADDNotice = new SqlCommand("insert into Notices values(@AdminID,@NoticeTitle,@NoticePath,getdate(),@Status)", con);
            Com_ADDNotice.Parameters.AddWithValue("@AdminID", Notice.AdminId);
            Com_ADDNotice.Parameters.AddWithValue("@NoticePath", "");
           // Com_ADDNotice.Parameters.AddWithValue("@NoticeDate", Notice.NoticeDate);
            Com_ADDNotice.Parameters.AddWithValue("@NoticeTitle", Notice.NoticeTitle);
            Com_ADDNotice.Parameters.AddWithValue("@Status", "new");
            con.Open();
            Com_ADDNotice.ExecuteNonQuery();


            SqlCommand com_getNoticeID = new SqlCommand("select @@identity", con);
            Notice.NoticeId =Convert.ToInt32( com_getNoticeID.ExecuteScalar());

            string FileAddress = "/Notices/" + Notice.NoticeId.ToString() +  Path.GetExtension(Notice.NoticePath.FileName);
            SqlCommand Com_UpdatePath = new SqlCommand("update Notices set NoticePath=@noticepath where NoticeId=@id", con);
            Com_UpdatePath.Parameters.AddWithValue("@noticepath", FileAddress);
            Com_UpdatePath.Parameters.AddWithValue("@id", Notice.NoticeId);
            Com_UpdatePath.ExecuteNonQuery();

            //con.Close();
            return Notice.NoticeId.ToString() + Path.GetExtension(Notice.NoticePath.FileName);
            }

            finally
            {
                con.Close();
            }
        }

        //////////////////////////////// code for inserting routine into database///////////////////////////////


        public String UploadRoutine(RoutineModel r,int _id)
        {
            try
            {

            //throw new NotImplementedException();
            r.TeacherId = _id;
            SqlCommand com_getCount = new SqlCommand("Select count(*) from RoutinesTable where ForWhichClass=@ofclass and ForWhichSection=@Section",con);
            com_getCount.Parameters.AddWithValue("@ofclass",r.RoutineForClass);
            com_getCount.Parameters.AddWithValue("@Section",r.RoutineForSection);
            con.Open();
            int co =Convert.ToInt32( com_getCount.ExecuteScalar());
            if(co>0)
            {
                
                string FileAddress = "/Routines/" + r.RoutineForClass + "_" + r.RoutineForSection + Path.GetExtension(r.File.FileName);
                SqlCommand com_UpdatePath = new SqlCommand("update RoutinesTable set RoutinesPath=@routinesPath where ForWhichClass=@ofclass and ForWhichSection=@Section ", con);
                com_UpdatePath.Parameters.AddWithValue("@routinesPath", FileAddress);
                com_UpdatePath.Parameters.AddWithValue("@ofclass", r.RoutineForClass);
                com_UpdatePath.Parameters.AddWithValue("@Section", r.RoutineForSection);
                com_UpdatePath.ExecuteNonQuery();
                con.Close();
                return r.RoutineForClass + "_" + r.RoutineForSection + Path.GetExtension(r.File.FileName);
            }
            else
            {
                SqlCommand com_addRoutine = new SqlCommand("insert into  RoutinesTable values(@TeacherID,@ForWhichClass,@ForWhichSection,@RoutinesPath,getdate())", con);
                com_addRoutine.Parameters.AddWithValue("@TeacherID", r.TeacherId);
                //com_addRoutine.Parameters.AddWithValue("@TeacherName", r.TeacherName);
                com_addRoutine.Parameters.AddWithValue("@ForWhichClass", r.RoutineForClass);
                com_addRoutine.Parameters.AddWithValue("@ForWhichSection", r.RoutineForSection);
                com_addRoutine.Parameters.AddWithValue("@RoutinesPath", "");
                //com_addRoutine.Parameters.AddWithValue("@UploadedDate",getdate());
                
                com_addRoutine.ExecuteNonQuery();


                //SqlCommand com_getRoutineID = new SqlCommand("select @@identity", con);
                //r.RoutineId = Convert.ToInt32(com_getRoutineID.ExecuteScalar());

                string FileAddress = "/Routines/" + r.RoutineForClass + "_" + r.RoutineForSection + Path.GetExtension(r.File.FileName);
                SqlCommand com_UpdatePath = new SqlCommand("update RoutinesTable set RoutinesPath=@routinesPath where ForWhichClass=@ofclass and ForWhichSection=@Section ", con);
                com_UpdatePath.Parameters.AddWithValue("@routinesPath", FileAddress);
                com_UpdatePath.Parameters.AddWithValue("@ofclass", r.RoutineForClass);
                com_UpdatePath.Parameters.AddWithValue("@Section", r.RoutineForSection);
                com_UpdatePath.ExecuteNonQuery();

                //n.Close();
                return r.RoutineForClass + "_" + r.RoutineForSection + Path.GetExtension(r.File.FileName);
            }
            }

            finally
            {
                con.Close();
            }
        }

        public int getSections(int enteredClass)
        {
            try { 
            SqlCommand com_getSec = new SqlCommand("select NoOfSection from ClassVsSections where class=@c", con);
            com_getSec.Parameters.AddWithValue("@c", enteredClass);
            con.Open();
            int c = Convert.ToInt32(com_getSec.ExecuteScalar());
            //con.Close();
            return c;
            }

            finally
            {
                con.Close();
            }
        }



        
        public bool updateNoOfSection(AddRemoveSectionModel model)
        {
            try
            {

            SqlCommand com_updateSec = new SqlCommand("update ClassVsSections set NoOfSection=@no where class=@cls", con);
            com_updateSec.Parameters.AddWithValue("@no", model.modifiedSection);
            com_updateSec.Parameters.AddWithValue("@cls", model.enteredClass);
            con.Open();

            int c = Convert.ToInt32(com_updateSec.ExecuteNonQuery());
           // con.Close();
            if (c == 1)
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

        public string uploadPhotoForGallery(GalleryUploadModel g_model)
        {
            try
            {

            SqlCommand com_galleryUpload = new SqlCommand("insert into Gallery values(@title,@imagePath,@imageDescription,@uploaderID,getDate())", con);
            com_galleryUpload.Parameters.AddWithValue("@title", g_model.title);
            com_galleryUpload.Parameters.AddWithValue("@imagePath", "");
            com_galleryUpload.Parameters.AddWithValue("@imageDescription", g_model.imageDescription);
            com_galleryUpload.Parameters.AddWithValue("@uploaderID", g_model.TeacherId);
            con.Open();
            com_galleryUpload.ExecuteNonQuery();

            SqlCommand com_getGalleryID = new SqlCommand("select @@identity", con);
            int id = Convert.ToInt32(com_getGalleryID.ExecuteScalar());
            string path = "/Gallery/" + g_model.TeacherId + "_" + id + Path.GetExtension(g_model.file.FileName);

            SqlCommand com_updatePath = new SqlCommand("update Gallery set imagePath=@path where imageID=@id", con);
            com_updatePath.Parameters.AddWithValue("@path", path);
            com_updatePath.Parameters.AddWithValue("@id", id);
            com_updatePath.ExecuteNonQuery();

            //con.Close();
            return g_model.TeacherId + "_" + id + Path.GetExtension(g_model.file.FileName);
            }

            finally
            {
                con.Close();
            }

        }


        public List<NotesModel> getNotesByTeacherID(int teacherID)
        {
            try
            { 
            SqlCommand com_getNotes = new SqlCommand("select NotesID, OfWhichClass, Section, NotesDesc from NotesTable where TeacherID=@tid", con);
            com_getNotes.Parameters.AddWithValue("@tid",teacherID);
            con.Open();
            SqlDataReader dr=  com_getNotes.ExecuteReader();
            List<NotesModel> lst_notes = new List<NotesModel>();
            while(dr.Read())
            {
                NotesModel n = new NotesModel();
                n.NotesID = dr.GetInt32(0);
                n.OfClass = dr.GetInt32(1);
                n.Section = dr.GetString(2);

                if (n.Section == "Science" || n.Section == "Arts" || n.Section == "Commerce")
                {
                    //string s = n.OfClass.ToString();
                    //string s1 = s[0].ToString();
                    //string s2 = s[1].ToString();
                    //string s3 = s1 +""+ s2;
                    n.OfClass =Convert.ToInt32( n.OfClass.ToString()[0] +""+ n.OfClass.ToString()[1]);
                }
                n.NoteSDesc = dr.GetString(3);
                lst_notes.Add(n);
            }
            return lst_notes;
            }

            finally
            {
                con.Close();
            }
        }



        public bool deleteNotes(int noteID)
        {
            SqlCommand com_dltNotes = new SqlCommand("delete NotesTable where notesID=@nid", con);
            com_dltNotes.Parameters.AddWithValue("@nid", noteID);
            con.Open();
            if (com_dltNotes.ExecuteNonQuery() == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }






        // /////////////////////////////////getting teacher details///////////////////////////////////////

        public TeacherModel getTeacher(int tID)
        {
            try
            {

            SqlCommand com_getTeacherDetails = new SqlCommand("select * from Teachers where TeacherID=@tID", con);
            com_getTeacherDetails.Parameters.AddWithValue("@tID", tID);
            con.Open();
            SqlDataReader dr = com_getTeacherDetails.ExecuteReader();
            TeacherModel getDetails = new TeacherModel();
            while (dr.Read())
            {

                getDetails.teacherName = dr.GetString(1);
                getDetails.teacherGender = dr.GetString(2);
                getDetails.dob = dr.GetDateTime(3).ToShortDateString();
                getDetails.teacherDesignation = dr.GetString(4);
                getDetails.TeacherQualification = dr.GetString(5);
                getDetails.subjectSpecilization = dr.GetString(6);
                getDetails.teacherAddress = dr.GetString(7);
                getDetails.teacherMobileNo = dr.GetString(8);
                getDetails.teacherEmailID = dr.GetString(9);
                getDetails.teacherPAN = dr.GetString(10);
                getDetails.teacherAadhar = dr.GetString(11);
                getDetails.noOfNotesUploaded = dr.GetInt32(12);
                getDetails.imageAddress = dr.GetString(13);
                //getDetails.passwordStatus = dr.GetString(13);
                //getDetails.teacherStatus = dr.GetString(14);
            }




           // con.Close();
            return getDetails;
            }

            finally
            {
                con.Close();
            }
        }



        // ////////////////////////////////////////upload news///////////////////////////////////////

        public string uploadNews(NewsUploadModel newsUploadModel)
        {
            try
            {

            SqlCommand com_uploadNews = new SqlCommand("insert into news values(@teacherID, getDate(), @newsTitle, @newsDescription, @newsDate, @newsImageAddress)", con);
            com_uploadNews.Parameters.AddWithValue("@teacherID", newsUploadModel.TeacherID);
            com_uploadNews.Parameters.AddWithValue("@newsTitle", newsUploadModel.newsTitle);
            com_uploadNews.Parameters.AddWithValue("@newsDescription", newsUploadModel.newsDescription);
            com_uploadNews.Parameters.AddWithValue("@newsDate", newsUploadModel.newsDate);
            com_uploadNews.Parameters.AddWithValue("@newsImageAddress", "");
            con.Open();
            com_uploadNews.ExecuteNonQuery();

            SqlCommand com_getNewsID = new SqlCommand("select @@identity", con);
            int id = Convert.ToInt32(com_getNewsID.ExecuteScalar());
            string path = "/News/" + newsUploadModel.TeacherID + "_" + id + Path.GetExtension(newsUploadModel.file.FileName);

            SqlCommand com_updatePath = new SqlCommand("update news set newsImageAddress=@path where newsID=@id", con);
            com_updatePath.Parameters.AddWithValue("@path", path);
            com_updatePath.Parameters.AddWithValue("@id", id);
            com_updatePath.ExecuteNonQuery();


            //con.Close();
            return newsUploadModel.TeacherID + "_" + id + Path.GetExtension(newsUploadModel.file.FileName);
            }

            finally
            {
                con.Close();
            }
        }



        ///////////////////////syudent list
        public List<StudentDataFetchModel> GetAllstudent(int ofClass)
        {
            try
            {

            SqlCommand com_GetAllStd = new SqlCommand("select * from Students where Class=@ofclass", con);
            com_GetAllStd.Parameters.AddWithValue("@ofclass", ofClass);
            con.Open();
            SqlDataReader dr = com_GetAllStd.ExecuteReader();
            List<StudentDataFetchModel> obj = new List<StudentDataFetchModel>();
            while (dr.Read())
            {

                StudentDataFetchModel t = new StudentDataFetchModel();
                t.StdId = dr.GetString(1);
                t.StdName = dr.GetString(2);
                t.DOB = Convert.ToDateTime(dr["DOB"]).ToString();
                t.gender = dr.GetString(4);
                t.Admsnyr = dr.GetInt32(5);
                t.GuardName = dr.GetString(6);
                t.MothrsName = dr.GetString(8);
                t.MobNO = dr.GetString(9);
                t.Address = dr.GetString(10);
                t.OfClass = dr.GetString(11);
                t.RStatus = dr.GetString(12);
                t.SStatus = dr.GetString(13);
                t.StdPass = dr.GetString(14);

                obj.Add(t);

            }

            //con.Close();
            return obj;
            }

            finally
            {
                con.Close();
            }


        }



        ////////////////update password souvik 09/11 ////////////////////////
        public bool updatePassword(int teacherID, ChangePasswordTeacherModel c)
        {
            try
            {
                con.Open();
                MembershipUser user = Membership.GetUser(teacherID.ToString());
                if (user.ChangePassword(c.oldPassword, c.newPassword))
                {
                    user.ChangePasswordQuestionAndAnswer(c.newPassword, c.securityQuestion, c.securityAnswer);

                    SqlCommand com_updatePasswordStatus = new SqlCommand("update Teachers set passwordStatus='Updated' where teacherID=@tid", con);
                    com_updatePasswordStatus.Parameters.AddWithValue("@tid", teacherID);
                    com_updatePasswordStatus.ExecuteNonQuery();
                    //  con.Close();
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

        ////////////////////////////////subha getting all events information////////////////
        public List<NewsModel> GetAllEvents(int id)
        {
            try
            {

                SqlCommand com_getAllEvents = new SqlCommand("select newsID, teacherID, uploadDate, newsTitle, newsDescription, newsDate from news where teacherID=@id order by newsDate desc", con);
                com_getAllEvents.Parameters.AddWithValue("@id", id);
            con.Open();
            SqlDataReader dr = com_getAllEvents.ExecuteReader();
            List<NewsModel> list = new List<NewsModel>();
            while (dr.Read())
            {
                NewsModel n_model = new NewsModel();
                n_model.newsID = dr.GetInt32(0);
                n_model.teacherID = dr.GetInt32(1);
                n_model.uploadDate = dr.GetDateTime(2).ToShortDateString();
                n_model.newsTitle = dr.GetString(3);
                n_model.newsDescription = dr.GetString(4);
                n_model.newsDate = dr.GetDateTime(5).ToShortDateString();
                list.Add(n_model);
            }

            //con.Close();
            return list;
            }

            finally
            {
                con.Close();
            }
        }

        /////////////////////////////////getting information of event by id////////////
        public NewsModel getEventDetails(int newsID)
        {
            try
            {

            SqlCommand com_getEvents = new SqlCommand("Select * from News where newsID=@nID", con);
            com_getEvents.Parameters.AddWithValue("@nID", newsID);
            con.Open();
            SqlDataReader dr = com_getEvents.ExecuteReader();
            NewsModel n_model = new NewsModel();
            while (dr.Read())
            {
                n_model.newsID = dr.GetInt32(0);
                n_model.teacherID = dr.GetInt32(1);
                n_model.newsTitle = dr.GetString(3);
                n_model.newsDescription = dr.GetString(4);
                n_model.newsDate = dr.GetDateTime(5).ToShortDateString();
                n_model.newsImageAddress = dr.GetString(6);
            }

            //con.Close();
            return n_model;
            }

            finally
            {
                con.Close();
            }
        }


        public string UpdateNews(NewsModel n_model)
        {
            try
            { 

            string path = "/News/" + n_model.teacherID + "_" + n_model.newsID + Path.GetExtension(n_model.file.FileName);
            SqlCommand com_updateNews = new SqlCommand("Update News set teacherID=@tID,uploadDate=getDate(),newsTitle=@nTitle,newsDescription=@nDes,newsDate=@nDate,newsImageAddress=@img where newsID=@nID", con);
            com_updateNews.Parameters.AddWithValue("@tID", n_model.teacherID);
            com_updateNews.Parameters.AddWithValue("@nTitle", n_model.newsTitle);
            com_updateNews.Parameters.AddWithValue("@nDes", n_model.newsDescription);
            com_updateNews.Parameters.AddWithValue("@nDate", n_model.newsDate);
            com_updateNews.Parameters.AddWithValue("@img", path);
            com_updateNews.Parameters.AddWithValue("@nID", n_model.newsID);
            con.Open();
            int count = com_updateNews.ExecuteNonQuery();
            //con.Close();
            if (count == 0)
            {
                return null;
            }
            else
            {
                return n_model.teacherID + "_" + n_model.newsID + Path.GetExtension(n_model.file.FileName);
            }
            }

            finally
            {
                con.Close();
            }
        }

        public int DeleteNews(int newsID)
        {
            try
            { 
            SqlCommand com_deleteNews = new SqlCommand("Delete from News where newsID=@nID", con);
            com_deleteNews.Parameters.AddWithValue("@nID", newsID);
            con.Open();
            int count = com_deleteNews.ExecuteNonQuery();
            //con.Close();
            return count;
            }

            finally
            {
                con.Close();
            }
        }

        //////delete gallery photo///
        public bool deletePhoto(int id)
        {
            try
            {
                SqlCommand com_delfromGallery = new SqlCommand("delete from Gallery where imageID=@Iid", con);
                com_delfromGallery.Parameters.AddWithValue("@Iid", id);
                con.Open();
                int Did = com_delfromGallery.ExecuteNonQuery();
                if (Did == 1)
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

        public List<ViewGalleryModel> getAllPhotos(int id)
        {
            try
            {
                SqlCommand com_getAllPhotos = new SqlCommand("select * from Gallery  where uploaderID=@id order by imageID desc", con);
                com_getAllPhotos.Parameters.AddWithValue("@id",id);
                con.Open();

                SqlDataReader dr = com_getAllPhotos.ExecuteReader();
                List<ViewGalleryModel> obj = new List<ViewGalleryModel>();

                while (dr.Read())
                {
                    ViewGalleryModel vgm = new ViewGalleryModel();
                    vgm.ImageId = dr.GetInt32(0);
                    vgm.title = dr.GetString(1);
                    vgm.imagePath = dr.GetString(2);
                    vgm.imageDescription = dr.GetString(3);
                    vgm.uploadDate = dr.GetDateTime(5).ToString();
                    vgm.uploaderID = dr.GetInt32(4);
                    obj.Add(vgm);
                }
                return obj;
            }
            finally
            {
                con.Close();
            }
        }

        /////////////////////////////////////getting image address by id for gallery///////////////
        public string getImageAddressGallery(int id)
        {
            try
            {
                SqlCommand com_getImage = new SqlCommand("Select imagePath from Gallery where imageID=@id", con);
                com_getImage.Parameters.AddWithValue("@id", id);
                con.Open();
                string path = com_getImage.ExecuteScalar().ToString();
                if (path != "")
                {
                    return path;
                }
                else
                {
                    return "false";
                }
            }
            finally
            {
                con.Close();
            }
        }

        public string getImageAddressNews(int id)
        {
            try
            {
                SqlCommand com_getImage = new SqlCommand("Select newsImageAddress from News where newsID=@id", con);
                com_getImage.Parameters.AddWithValue("@id", id);
                con.Open();
                string path = com_getImage.ExecuteScalar().ToString();
                if (path != "")
                {
                    return path;
                }
                else
                {
                    return "false";
                }
            }
            finally
            {
                con.Close();
            }
        }

        public string getImageAddressNotes(int id)
        {
            try
            {
                SqlCommand com_getImage = new SqlCommand("Select NotesPath from NotesTable where NotesID=@id", con);
                com_getImage.Parameters.AddWithValue("@id", id);
                con.Open();
                string path = com_getImage.ExecuteScalar().ToString();
                if (path != "")
                {
                    return path;
                }
                else
                {
                    return "false";
                }
            }
            finally
            {
                con.Close();
            }
        }

        //////////// subha 21/11/17///

        public List<NoticeModel> getAllNotices()
        {
            try
            {
                SqlCommand com_getAllNotices = new SqlCommand("Select * from notices", con);
                con.Open();
                SqlDataReader dr = com_getAllNotices.ExecuteReader();
                List<NoticeModel> list = new List<NoticeModel>();
                while (dr.Read())
                {
                    NoticeModel n_model = new NoticeModel();
                    n_model.NoticeId = dr.GetInt32(0);
                    n_model.AdminId = dr.GetInt32(1);
                    n_model.NoticeTitle = dr.GetString(2);
                    n_model.noticeImgPath = dr.GetString(3);
                    n_model.noticeUploadDate = dr.GetDateTime(4).ToString();
                    list.Add(n_model);
                }
               

                return list;
            }
            finally
            {
                con.Close();
            }

        }

        public string getNoticeImgPath(int id)
        {
            try
            {
            SqlCommand com_getImage = new SqlCommand("Select NoticePath from notices where noticeid=@id", con);
            com_getImage.Parameters.AddWithValue("@id", id);
            con.Open();
            string path = com_getImage.ExecuteScalar().ToString();
            
            return path;
            }
            finally
            {
                con.Close();
            }
        }

        public bool deleteNotice(int id)
        {
            try
            {
            SqlCommand com_deleteNotice = new SqlCommand("delete from notices where noticeid=@id", con);
            com_deleteNotice.Parameters.AddWithValue("@id", id);
            con.Open();
            int count = com_deleteNotice.ExecuteNonQuery();
            
            if (count == 1)
            {
                return true;
            }
            else
                return false;

            }
            finally
            {
                con.Close();
            }

        }
    }
}