using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_FKTHSchool_Project.Models;
using System.Web.Security;

namespace Mvc_FKTHSchool_Project.Controllers
{
    [Authorize(Roles = "Teacher")]
    public class PublicViewController : Controller
    {
        //
        // GET: /PublicView/


        [AllowAnonymous]
        public ActionResult Index()
        {
            //try
            //{ 
            PublicViewDAL pdal = new PublicViewDAL();
            List<ViewToppersListModel> obj = pdal.TopperPicLoad();
            List<NoticeDownloadModel> list_all = pdal.getNotice();

            return View(new MyModel { listOfNotice = list_all, listOfToppers = obj });
            //}
            //catch (Exception)
            //{
            //    return View("Error");
            //}
        }


     
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

         [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LogInModel model)
        {
             try
             { 
            LoginDAL dal = new LoginDAL();
            //string s1 = model.category;
            if(model.category=="Teacher")
            {
                if (model.LogInId.ToString().Length == 4)
                {
                    CheckPasswordStatusEnum status =(CheckPasswordStatusEnum) dal.loginTeacher(model);
                    if (status == CheckPasswordStatusEnum.WrongPassword)
                    {
                        ViewBag.errmsg = "Invalid login ID or password!";
                        return View();
                    }
                    else if (status == CheckPasswordStatusEnum.NewUser)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("UpdatePasswordTeacher");
                    }
                    else if (status == CheckPasswordStatusEnum.Updated)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("Index", "Teacher");
                    }
                }
                ViewBag.errmsg = "Enter Valid Login ID";
                return View();
            }
            else if (model.category == "Student")
            {
                //student login check
                if (model.LogInId.Length == 8 && (model.LogInId[0] == 'S' || model.LogInId[0] == 's'))
                {
                    int count = Convert.ToInt32(new LoginDAL().studentLogIn(model));
                    if (count == 1)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("StudentProfile", "RegisteredView");
                    }
                    else
                    {
                        ViewBag.errmsg = "Invalid LogIn Id or Password";
                        return View();
                       
                    }

                }
                else
                {
                    ViewBag.errmsg = "Invalid Login ID";
                    return View();
                }

            }
            else
            {
                ViewBag.errmsg = "Invalid login ID or password!";
                return View();
            }
             }
             catch (Exception)
             {
                 return View("Error");
             }
           
        }


        [HttpGet]
        public ActionResult UpdatePasswordTeacher()
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
        public ActionResult UpdatePasswordTeacher(UpdatePasswordTeacherModel model)
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


            int teacherID=Convert.ToInt32(User.Identity.Name);
            LoginDAL dal = new LoginDAL();
            if(dal.updatePassword(teacherID, model))
            {
                return View("Updated");
            }
            return View("NotUpdated");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        
        [AllowAnonymous]
        public ActionResult GetDevelopers()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ViewGallery()
        {
            try { 
            PublicViewDAL pDAL = new PublicViewDAL();
            List<ViewGalleryModel> list_gallery = pDAL.getAllPhotos();
            return View(list_gallery);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult AllTeachers()
        {
            //try
            //{ 
            PublicViewDAL pDAL = new PublicViewDAL();
            List<GetAllTeacherModel> list_allTeachers = pDAL.getAllTeachers();
            return View(list_allTeachers);
            //}
            //catch (Exception)
            //{
            //    return View("Error");
            //}
        }

        [AllowAnonymous]
        public ActionResult News()
        {
            try
            {
                PublicViewDAL pDAL = new PublicViewDAL();
                List<NewsModel> list_news = pDAL.getAllNews();
                return View(list_news);
            }
            catch(Exception)
            {
                return View("Error");
            }
        }






           ///////////////////////////////////////// souvik 10/11 forget password ///////////////////////////
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgetPassword() // teacher
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

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPasswordModel model)
        {
            try
            {
                LoginDAL dal = new LoginDAL();
                string ps = dal.forgetPassword(model);
                if (ps == null)
                {

                    List<SelectListItem> lst_securityQuestion = new List<SelectListItem>();
                    lst_securityQuestion.Add(new SelectListItem { Text = "Select", Value = "" });
                    lst_securityQuestion.Add(new SelectListItem { Text = "What is your's first school name?", Value = "What is your's first school name?" });
                    lst_securityQuestion.Add(new SelectListItem { Text = "What is your pet’s name?", Value = "What is your pet’s name?" });
                    lst_securityQuestion.Add(new SelectListItem { Text = "In what city or town does your nearest sibling live?", Value = "In what city or town does your nearest sibling live?" });
                    lst_securityQuestion.Add(new SelectListItem { Text = "What is your favorite food?", Value = "What is your favorite food?" });

                    ViewBag.securityQuestion1 = lst_securityQuestion;



                    ViewBag.err = "Wrong Security answer";
                    return View();
                }
                else
                {
                    ViewBag.passwrd = ps;
                    return View("PasswordUpdated");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        ////////////////////////subha 12/11/2017////////////////////////////////////
        [AllowAnonymous]
        public ActionResult ViewAlumni()
        {
            try
            { 
            PublicViewDAL pDAL = new PublicViewDAL();
            List<AlumniModel> list_viewAllAlumni = pDAL.ViewAllAlumni();
            if (list_viewAllAlumni.Count == 0)
            {
                return RedirectToAction("Index");
            }

            return View(list_viewAllAlumni);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        ///////////////////////////////subha 14/11/2017///////////////////////

        [AllowAnonymous]
        [HttpGet]
        public ActionResult ViewOC()
        {
            AdminDAL aDAL = new AdminDAL();
            List<OCModel> list_viewAllOC = aDAL.GetAllOC();

            return View(list_viewAllOC);
        }
      

    }
}

   