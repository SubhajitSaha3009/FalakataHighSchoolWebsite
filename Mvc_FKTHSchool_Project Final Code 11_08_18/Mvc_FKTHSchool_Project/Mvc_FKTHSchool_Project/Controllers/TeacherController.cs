using Mvc_FKTHSchool_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Mvc_FKTHSchool_Project.Controllers
{
    [Authorize(Roles = "Teacher")]
   public class TeacherController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            try
            { 
            int tID = Convert.ToInt32(User.Identity.Name);
            TeacherDAL tDAL = new TeacherDAL();
            TeacherModel g_tDetails = tDAL.getTeacher(tID);

            ViewBag.msg = TempData["updatedmsg"];
            return View(g_tDetails);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult NotesUpload()
        {
            try
            {

            List<SelectListItem> list_classes = new List<SelectListItem>();
            list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
            list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
            list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
            list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
            list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
            list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
            list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
            list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
            list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
            list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
            list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
            list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
            list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

            ViewBag.classSelection = list_classes;

            List<SelectListItem> list_sections = new List<SelectListItem>();
            list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
            list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
            list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
            list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
            list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
            list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
            list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
            list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


            ViewBag.sec = list_sections;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        private bool isValidContentNotes(String ContentType)
        {
            return ContentType.Equals("application/pdf") || ContentType.Equals("image/jpeg") || ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") || ContentType.Equals("application/msword") || ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") || ContentType.Equals("image/jpg");
        }

       [HttpPost]
        public ActionResult NotesUpload(NotesModel N)
        {
           try
           {
          // ModelState.Clear();
           bool flag=isValidContentNotes(N.file.ContentType);
           if(!flag)
           {

               List<SelectListItem> list_classes = new List<SelectListItem>();
               list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
               list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
               list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
               list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
               list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
               list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
               list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

               ViewBag.classSelection = list_classes;



               List<SelectListItem> list_sections = new List<SelectListItem>();
               list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
               list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
               list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
               list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
               list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
               list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
               list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
               list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


               ViewBag.sec = list_sections;



               ViewBag.Error = "Only pdf,jpg,docx,doc,xlns are allowed";
               return View();


           }

           if (N.file.ContentLength > 5000000)
           {

               List<SelectListItem> list_classes = new List<SelectListItem>();
               list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
               list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
               list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
               list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
               list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
               list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
               list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
               list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
               list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

               ViewBag.classSelection = list_classes;



               List<SelectListItem> list_sections = new List<SelectListItem>();
               list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
               list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
               list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
               list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
               list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
               list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
               list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
               list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


               ViewBag.sec = list_sections;

               ViewBag.Error = "File Size Must Be Less Than 5mb";
               return View();
           }

               TeacherDAL dal = new TeacherDAL();
               int noOfSec = dal.getSections(N.OfClass);

               if (N.OfClass != 111 && N.OfClass != 112 && N.OfClass != 113 && N.OfClass != 121 && N.OfClass != 122 && N.OfClass != 12 && N.OfClass != 123)
               {
                   if (N.Section == "B" && noOfSec < 2 || N.Section == "C" && noOfSec < 3 || N.Section == "D" && noOfSec < 4 || N.Section == "Science" || N.Section == "Arts" || N.Section == "Commerce")
                   {

                       List<SelectListItem> list_classes = new List<SelectListItem>();
                       list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
                       list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
                       list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
                       list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
                       list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
                       list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

                       ViewBag.classSelection = list_classes;



                       List<SelectListItem> list_sections = new List<SelectListItem>();
                       list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
                       list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
                       list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
                       list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
                       list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
                       list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
                       list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


                       ViewBag.sec = list_sections;



                       ViewBag.errmsg = "Invalid Section";
                       return View();
                   }
               }

               if (N.OfClass == 111 || N.OfClass == 112 || N.OfClass == 113 || N.OfClass == 121 || N.OfClass == 122 || N.OfClass == 12 || N.OfClass == 123)
               {
                   if (N.Section == "A" || N.Section == "B" || N.Section == "C" || N.Section == "D")
                   {
                       List<SelectListItem> list_classes = new List<SelectListItem>();
                       list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
                       list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
                       list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
                       list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
                       list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
                       list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

                       ViewBag.classSelection = list_classes;



                       List<SelectListItem> list_sections = new List<SelectListItem>();
                       list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
                       list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
                       list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
                       list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
                       list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
                       list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
                       list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


                       ViewBag.sec = list_sections;



                       ViewBag.errmsg = "Invalid Section";
                       return View();
                   }
                   if ((N.OfClass == 111 && N.Section != "Science") || (N.OfClass == 112 && N.Section != "Arts") || (N.OfClass == 113 && N.Section != "Commerce") || (N.OfClass == 121 && N.Section != "Science") || (N.OfClass == 122 && N.Section != "Arts") || (N.OfClass == 123 && N.Section != "Commerce"))
                   {
                       List<SelectListItem> list_classes = new List<SelectListItem>();
                       list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
                       list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
                       list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
                       list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
                       list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
                       list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
                       list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
                       list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

                       ViewBag.classSelection = list_classes;



                       List<SelectListItem> list_sections = new List<SelectListItem>();
                       list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
                       list_sections.Add(new SelectListItem { Text = "A", Value = "A" });
                       list_sections.Add(new SelectListItem { Text = "B", Value = "B" });
                       list_sections.Add(new SelectListItem { Text = "C", Value = "C" });
                       list_sections.Add(new SelectListItem { Text = "D", Value = "D" });
                       list_sections.Add(new SelectListItem { Text = "Science", Value = "Science" });
                       list_sections.Add(new SelectListItem { Text = "Arts", Value = "Arts" });
                       list_sections.Add(new SelectListItem { Text = "Commerce", Value = "Commerce" });


                       ViewBag.sec = list_sections;



                       ViewBag.errmsg = "Invalid Section";
                       return View();
                   }
               }

               int id = Convert.ToInt32(User.Identity.Name);
               TeacherDAL t = new TeacherDAL();
               var Filename = t.UploadNotes(N,id);
               var path = Path.Combine(Server.MapPath("~/Notes/"), Filename);
               N.file.SaveAs(path);
               ViewBag.Redirectionmsg = "Notes Uploaded Successfully.";
               ViewBag.Btnlbl = "Upload Another Note";
               ViewBag.BtnAction = "NotesUpload";
               return View("Success");
           }
           catch (Exception)
           {
               return View("Error");
           }    
        }
       private bool isValidContentNotice(String ContentType)
       {
           return ContentType.Equals("application/pdf") || ContentType.Equals("image/jpeg") || ContentType.Equals("application/vnd.openxmlformats-officedocument.wordprocessingml.document") || ContentType.Equals("application/msword") || ContentType.Equals("image/jpg") || ContentType.Equals("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
       }

       

       public ActionResult Logout()
       {
           FormsAuthentication.SignOut();
           return RedirectToAction("Login", "PublicView");
       }



       ////////////////////////For Fetching all Student List//////////////////////////
        ///////////////////911/////////////////////////////
       [HttpGet]
       public ActionResult GetAllStudentList()
       {
           try
           { 
           List<SelectListItem> list_classes = new List<SelectListItem>();
           list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
           list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
           list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
           list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
           list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
           list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
           list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
           list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
           list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
           list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
           list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
           list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
           list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });
           ViewBag.classSelection = list_classes;

           return View();
           }
           catch (Exception)
           {
               return View("Error");
           }
       }

       [HttpPost]
       public ActionResult GetAllStudentList(FetchStudentByClassModel m)
       {
           try
           { 
           TempData["StdClass"] = m.ofClass;
           return RedirectToAction("AllStudent");
           }
           catch (Exception)
           {
               return View("Error");
           }
       }

       [HttpGet]
       public ActionResult AllStudent()
       {
           try
           { 
           TeacherDAL Sdal = new TeacherDAL();
           int OfCLass = Convert.ToInt32(TempData["StdClass"]);
           ViewBag.Class = OfCLass.ToString();
           if (OfCLass != 0)
           {
               List<StudentDataFetchModel> obj = Sdal.GetAllstudent(OfCLass);
               return View(obj);
           }
           else
               return RedirectToAction("GetAllStudentList");
           }
           catch (Exception)
           {
               return View("Error");
           }
       }



       [HttpGet]
       public ActionResult UploadForGallery()
       {
           try
           { 
          // TempData["teacherID"] = User.Identity.Name;
           if (User.Identity.Name.ToString() != "")
           {
               return View();
           }
           else
           {
               return RedirectToAction("Login", "PublicView");
           }
           }
           catch (Exception)
           {
               return View("Error");
           }
       }

       [HttpPost]
       public ActionResult UploadForGallery(GalleryUploadModel g_model)
       {
           try
           {
               //ModelState.Clear();
               if (!isValidContentProfilePic(g_model.file.ContentType))
           {
               ViewBag.Error = "Only png,jpg are allowed";
               return View();
           }
               if (g_model.file.ContentLength > 8000000)
           {
               ViewBag.Error = "File Size Must Be Less Than 8MB";
               return View();
           }

               TeacherDAL tDAL = new TeacherDAL();
               string filename = tDAL.uploadPhotoForGallery(g_model);
               var path = Path.Combine(Server.MapPath("~/Gallery/"), filename);
               g_model.file.SaveAs(path);
               ViewBag.Redirectionmsg = "Photo Uploaded to the Gallery Successfully.";
               ViewBag.Btnlbl = "Upload Another Photo";
               ViewBag.BtnAction = "UploadForGallery";
               return View("Success");
           }
           catch (Exception)
           {
               return View("Error");
           }
           
       }


        [HttpGet]
       public ActionResult DeleteNotes()
       {
           try
           {
               TeacherDAL dal = new TeacherDAL();
               List<NotesModel> lst_notes = dal.getNotesByTeacherID(Convert.ToInt32(User.Identity.Name));
               ViewBag.msg = TempData["msg"];
               ViewBag.emsg = TempData["emsg"];
               return View(lst_notes);
           }
           catch (Exception)
           {
               return View("Error");
            }
       }


       // [HttpPost]
        public ActionResult DeleteN(int id)
        {
            try
            { 
            TeacherDAL dal = new TeacherDAL();
            string path = dal.getImageAddressNotes(id);
            string Path = Request.MapPath("~" + path);

            if (dal.deleteNotes(id))
            {
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }
                TempData["msg"] = "Note Deleted Successfully";
                return RedirectToAction("DeleteNotes");
            }
            else
            {
                TempData["emsg"] = "Note not Deleted";
                return RedirectToAction("DeleteNotes");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }






        private bool isValidContentProfilePic(String ContentType)
        {
            return ContentType.Equals("image/jpeg") || ContentType.Equals("image/png") || ContentType.Equals("image/jpg");
        }


        [HttpGet]
        public ActionResult UploadNews()
        {

            return View();
        }

        [HttpPost]
        public ActionResult UploadNews(NewsUploadModel newsUpload, HttpPostedFileBase imageAddress)
        {
            try
            {
                newsUpload.file = imageAddress;
               // ModelState.Clear();
                if (!isValidContentProfilePic(imageAddress.ContentType))
            {
                ViewBag.Error = "Only png,jpg are allowed";
                return View();
            }
            if (imageAddress.ContentLength > 8000000)
            {
                ViewBag.Error = "File Size Must Be Less Than 8MB";
                return View();
            }
                TeacherDAL tDAL = new TeacherDAL();
                string filename = tDAL.uploadNews(newsUpload);
                var path = Path.Combine(Server.MapPath("~/News/"), filename);
                imageAddress.SaveAs(path);
                ViewBag.Redirectionmsg = "News Uploaded Successfully.";
                ViewBag.Btnlbl = "Upload Another News";
                ViewBag.BtnAction = "UploadNews";
                return View("Success");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }




        ////////////////////////////////Update password souvik 09/11 /////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult UpdatePassworTeacher()
        {
            try
            { 
            List<SelectListItem> lst_securityQuestion = new List<SelectListItem>();
            lst_securityQuestion.Add(new SelectListItem { Text = "Select", Value = "" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your's first school name?", Value = "What is your's first school name?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your pet’s name?", Value = "What is your pet’s name?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "In what city or town does your nearest sibling live?", Value = "In what city or town does your nearest sibling live?" });
            lst_securityQuestion.Add(new SelectListItem { Text = "What is your favorite food?", Value = "What is your favorite food?" });

            ViewBag.securityQuestion1 = lst_securityQuestion;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult UpdatePassworTeacher(ChangePasswordTeacherModel model)
        {
            try
            {
                List<SelectListItem> lst_securityQuestion = new List<SelectListItem>();
                lst_securityQuestion.Add(new SelectListItem { Text = "Select", Value = "" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your's first school name?", Value = "What is your's first school name?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your pet’s name?", Value = "What is your pet’s name?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "In what city or town does your nearest sibling live?", Value = "In what city or town does your nearest sibling live?" });
                lst_securityQuestion.Add(new SelectListItem { Text = "What is your favorite food?", Value = "What is your favorite food?" });

                ViewBag.securityQuestion1 = lst_securityQuestion;


                int teacherID = Convert.ToInt32(User.Identity.Name);
                TeacherDAL dal = new TeacherDAL();
                if (dal.updatePassword(teacherID, model))
                {
                    TempData["updatedmsg"] = "Password is updated";
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.errmsg = "Wrong old password!";
                    return View();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        //////////////////////////////subha 11/11/17 for events update////////////////////
        [HttpGet]
        public ActionResult EventsUpdateTable()
        {
            try
            { 
            TeacherDAL tDAL = new TeacherDAL();
            List<NewsModel> list_events = tDAL.GetAllEvents(Convert.ToInt32(User.Identity.Name));

            if (list_events.Count == 0)
            {
                ViewBag.noEventsFound = "No Events Found";
                ViewBag.dlt=TempData["dlt"];
                //return RedirectToAction("Fail");
                return View(list_events);
                ///// chambol redirection
            }
           else
            {
                ViewBag.dlt = TempData["dlt"];
                //return RedirectToAction("Fail");
                return View(list_events);

            }

            //return View(list_events);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult UpdateNews(int newsID)
        {
            try
            { 
            if (newsID.ToString() == "")
            {
                return RedirectToAction("EventsUpdateTable");
            }

            TeacherDAL tDAL = new TeacherDAL();
            NewsModel n_model = tDAL.getEventDetails(newsID);
            if (n_model.newsID.ToString() == null)
            {
                return RedirectToAction("EventsUpdateTable");
            }
            else
            {
                ViewBag.eID = n_model.newsID;
                //ViewBag.tID = n_model.teacherID;
                ViewBag.eTitle = n_model.newsTitle;
                ViewBag.eDes = n_model.newsDescription;
                ViewBag.eDate = n_model.newsDate;
                ViewBag.eImg = n_model.newsImageAddress;
                return View(n_model);
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult UpdateNews(NewsModel n_model)
        {
            ////////////////////////// chambol success redirection
            try
            { 
            ViewBag.eID = n_model.newsID;
            //ViewBag.tID = n_model.teacherID;
            ViewBag.eTitle = n_model.newsTitle;
            ViewBag.eDes = n_model.newsDescription;
            ViewBag.eDate = n_model.newsDate;
            ViewBag.eImg = n_model.newsImageAddress;

            if (!isValidContentProfilePic(n_model.file.ContentType))
            {
                ViewBag.Error = "Only png,jpg are allowed";
                return View();
            }
            if (n_model.file.ContentLength > 8000000){
                ViewBag.Error = "File Size Must Be Less Than 8mb";
                return View();
            }
            else
            {
                TeacherDAL tDAL = new TeacherDAL();
                string filename = tDAL.UpdateNews(n_model);
                var path = Path.Combine(Server.MapPath("~/News/"), filename);
                n_model.file.SaveAs(path);
                ViewBag.Redirectionmsg = "News Updated Successfully.";
                ViewBag.Btnlbl = "Update Another News";
                ViewBag.BtnAction = "UpdateNews";
                return View("Success");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult DeleteNews(int newsID)
        {
            try
            { 
            TeacherDAL tDAL = new TeacherDAL();
            string path = tDAL.getImageAddressNews(newsID);

            string Path = Request.MapPath("~" + path);
            int count = tDAL.DeleteNews(newsID);
            if (count == 1)
            {
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }
                TempData["dlt"] = "Events Deleted Succesfully";
                //success redirection champa
               // string s = (string)ViewBag.dlt;
                return RedirectToAction("EventsUpdateTable");
            }
            else
            {
                //fail
                ViewBag.Redirectionmsg = "News was not deleted.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "UpdateNews";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }



        //////////////////Delete from gallery///////////////////

        public ActionResult ViewAllPhotos()
        {
            TeacherDAL tDAL = new TeacherDAL();
            List<ViewGalleryModel> obj = tDAL.getAllPhotos(Convert.ToInt32(User.Identity.Name));
            ViewBag.Msg = TempData["PhotoDelmsg"];
            ViewBag.eMsg = TempData["PhotoDelemsg"];
            return View(obj);
        }

        public ActionResult DeletePhotos(int id)
        {
            TeacherDAL tDal = new TeacherDAL();
            string path = tDal.getImageAddressGallery(id);

            string Path = Request.MapPath("~" + path);

            if (tDal.deletePhoto(id))
            {
                if (System.IO.File.Exists(Path))
                {
                    System.IO.File.Delete(Path);
                }
                TempData["PhotoDelmsg"] = "Photo Deleted Successfully.";
                return RedirectToAction("ViewAllPhotos");
            }
            else
            {
                TempData["PhotoDelemsg"] = "Photo was not Deleted, Try Again.";
                return RedirectToAction("ViewAllPhotos");
            }
        }
      
    }

    }
