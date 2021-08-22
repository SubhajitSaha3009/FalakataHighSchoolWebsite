using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_FKTHSchool_Project.Models;
using System.IO;
using System.Web.Security;
namespace Mvc_FKTHSchool_Project.Controllers
{
    [Authorize(Roles = "Student")]
    public class RegisteredViewController : Controller
    {
        //
        // GET: /RegisteredView/
        RegisteredDal dal = new RegisteredDal();
        public ActionResult StudentProfile()
        {
            try
            {

            StudentModel sm = dal.GetProfile(User.Identity.Name);
            ViewBag.StdName = sm.name;
            ViewBag.ClassStd = sm.class_name;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult StudentProfile(FeedbackModel model)
        {
            try
            {
                model.SenderID = User.Identity.Name;
                if (ModelState.IsValid)
                {
                    if (dal.sendfeedback(model))
                    {
                        ViewBag.msg = "Feedback sent";
                        ModelState.Clear();
                        //return Json(new { Success = true, Message = "" });
                        return RedirectToAction("StudentProfile");
                    }
                    else
                    {
                        ViewBag.msg = "Feedback not sent";
                        ModelState.Clear();
                        return RedirectToAction("Error");
                    }
                }
                else
                {
                    ViewBag.msg = "Feedback not sent";
                    ModelState.Clear();
                    // return Json(new { Success = false, Message = "" }); 
                    return RedirectToAction("Error");
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }





        [HttpGet]

        public ActionResult DownloadNotesbyCalss()
        {
            return View();
        }

      

       [HttpGet]
        public ActionResult NoteDwnld()
        {
           try
           { 
            RegisteredDal Rdal = new RegisteredDal();
            string s = User.Identity.Name;
            if (s != "")
            {
                // int StdId=Convert.ToInt32(s);
                List<NotesDownloadModel> N1 = Rdal.GetNotesbyStdClass(s);
                if (N1.Count > 0)
                    return View(N1);
                else
                    return View("NotesNotFound");
            }
            else
                return RedirectToAction("Login", "PublicView");

           }
           catch (Exception)
           {
               return View("Error");
           }

        }

       

        [HttpGet]
        public ActionResult RoutineDownload()
        {
            try
            { 
            if(TempData["ErrMsg"]==null)
            {
                ViewBag.Error = "";
            }
            else
            {
                ViewBag.Error=Convert.ToString(TempData["ErrMsg"]);
            }
            //List<SelectListItem> list_classes = new List<SelectListItem>();
            //list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
            //list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
            //list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
            //list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
            //list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
            //list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
            //list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });
            //list_classes.Add(new SelectListItem { Text = "class- XI- Science", Value = "111" });
            //list_classes.Add(new SelectListItem { Text = "class- XI- Arts", Value = "112" });
            //list_classes.Add(new SelectListItem { Text = "class- XI- Commerce", Value = "113" });
            //list_classes.Add(new SelectListItem { Text = "class- XII- Science", Value = "121" });
            //list_classes.Add(new SelectListItem { Text = "class- XII- Arts", Value = "122" });
            //list_classes.Add(new SelectListItem { Text = "class- XII- Commerce", Value = "123" });

            //ViewBag.classSelection = list_classes;

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

        [HttpPost]
        public ActionResult RoutineDownload(RoutineDownloadModel rdm)
        {
            try
            { 
            //TempData["forClass"]=rdm.RoutineForClass;
            TempData["forSec"] = rdm.RoutineForSection;
            return RedirectToAction("RoutinedwnldPage");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult RoutinedwnldPage()
        {
            try
            { 
            if (TempData["forSec"].ToString() != "")
            {
                RoutineDownloadModel r = new RoutineDownloadModel();
                //r.RoutineForClass = Convert.ToString(TempData["forClass"]);
                r.RoutineForSection = Convert.ToString(TempData["forSec"]);
                RegisteredDal rdal = new RegisteredDal();
                string RPath = rdal.GetRoutine(User.Identity.Name, r.RoutineForSection);
                if (RPath.Equals("fail"))
                {
                    TempData["ErrMsg"] = "No Routine Uploaded for section " + r.RoutineForSection;
                    return RedirectToAction("RoutineDownload");
                }
                //ViewBag.OfClass = r.RoutineForClass;
                ViewBag.OfSec = r.RoutineForSection;
                ViewBag.Rpath = RPath;
                return View();
            }
            else
                return RedirectToAction("RoutineDownload");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult StudentHome()
        {
            return View();
        }



        public ActionResult Logout()
        {
            try
            { 
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "PublicView");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

    }
}
