using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mvc_FKTHSchool_Project.Models;
using System.Web.Security;
using System.IO;

namespace Mvc_FKTHSchool_Project.Controllers
{
   [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        

        AdminDAL dal = new AdminDAL();
        public ActionResult Index()
        {
            try
            {
                AdminModel model = dal.getAdminDetails(Convert.ToInt32(User.Identity.Name));
                ViewBag.nm = model.adminName;
                ViewBag.desig = model.adminDesignation;
                ViewBag.img = model.imageAddress;

                ViewBag.msg = TempData["updatedmsg"];
                return View();
            }
            catch(Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult AddTeacher()
        {
            return View();
        }

        private bool isValidContentProfilePic(String ContentType)
        {
            return ContentType.Equals("image/jpeg") || ContentType.Equals("image/png") || ContentType.Equals("image/jpg");
        }
        [HttpPost]
        public ActionResult AddTeacher(TeacherModel model, HttpPostedFileBase imageAddress)
        {
            try
            { 
            if (imageAddress != null)
            {
                if (!isValidContentProfilePic(imageAddress.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";
                    return View();
                }

                if (imageAddress.ContentLength > 5000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 5mb";
                    return View();
                }
               
            }


            if (ModelState.IsValid)
            {

                string idpassword = dal.addTeacher(model,imageAddress);
                string[] arr = idpassword.Split(' ');


                ViewBag.id = arr[0];
                ViewBag.password = arr[1];
               

                
                if (imageAddress != null)
                {
                    imageAddress.SaveAs(Server.MapPath("~/TeacherImages/" + model.teacherID + Path.GetExtension(imageAddress.FileName)));  
                }
                return View("TeacherAdded");
            }
            else
            {
                ViewBag.Redirectionmsg = "Teacher not Added.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AddTeacher";
                return View("Fail");
                //return View("TeacherNotAdded");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

           
        }


        [HttpGet]
        public ActionResult UpdateTeacherProfile(int id)
        {
            try
            {

             List<SelectListItem> lst = new List<SelectListItem>();
         

            int teacherID = id; //Convert.ToInt32(TempData["teachid"]);
            if (teacherID != 0)
            {
                TeacherModel model = dal.viewTeacherProfile(teacherID);
                ViewBag.id = model.teacherID;
                ViewBag.name = model.teacherName;
                ViewBag.designation = model.teacherDesignation;
                ViewBag.specialisation = model.subjectSpecilization;
                ViewBag.address = model.teacherAddress;
                ViewBag.mbl = model.teacherMobileNo;
                ViewBag.email = model.teacherEmailID;
                ViewBag.pan = model.teacherPAN;
                ViewBag.aadhar = model.teacherAadhar;
                ViewBag.qual = model.TeacherQualification;
                ViewBag.aad = model.teacherAadhar;
                ViewBag.panNumber = model.teacherPAN;



                if (model.teacherStatus == "Current teacher" || (model.teacherStatus == "current teacher"))
                {
                    lst.Add(new SelectListItem { Text = "Current teacher", Value = "Current teacher" });
                    lst.Add(new SelectListItem { Text = "Retired teacher", Value = "retired teacher" });
                }
                else
                {
                    lst.Add(new SelectListItem { Text = "Retired teacher", Value = "retired teacher" });
                    lst.Add(new SelectListItem { Text = "Current teacher", Value = "Current teacher" });
                }

                ViewBag.status = lst;
                TempData["teachid"] = teacherID;
                return View();
            }
            else
            {
                /////where to redirect
                return RedirectToAction("GetAllStudentList");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
            
        }
        [HttpPost]
        public ActionResult UpdateTeacherProfile(UpdateTeacherModel model, HttpPostedFileBase imageAddress)
        {
           //try
           //{ 
            if (imageAddress != null)
            {
                if (!isValidContentProfilePic(imageAddress.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";

                    List<string> list_status = dal.getTeacherStatus();
                    List<SelectListItem> lst = new List<SelectListItem>();
                    lst.Add(new SelectListItem { Text = "Select", Value = "" });
                    foreach (string i in list_status)
                    {
                        lst.Add(new SelectListItem { Text = i, Value = i });
                    }
                    ViewBag.status = lst;

                    int teacherID = model.teacherID;
                    TeacherModel model1 = dal.viewTeacherProfile(teacherID);
                    ViewBag.id = model1.teacherID;
                    ViewBag.name = model1.teacherName;
                    ViewBag.designation = model1.teacherDesignation;
                    ViewBag.specialisation = model1.subjectSpecilization;
                    ViewBag.address = model1.teacherAddress;
                    ViewBag.mbl = model1.teacherMobileNo;
                    ViewBag.email = model1.teacherEmailID;

                    TempData["teachid"] = teacherID;

                    return View();
                }

                if (imageAddress.ContentLength > 5000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 5mb";
                   
                        List<string> list_status = dal.getTeacherStatus();
                        List<SelectListItem> lst = new List<SelectListItem>();
                        //lst.Add(new SelectListItem { Text = "Select", Value = "" });
                        //foreach (string i in list_status)
                        //{
                        //    lst.Add(new SelectListItem { Text = i, Value = i });
                        //}

                        if (model.teacherStatus == "Current teacher" || (model.teacherStatus == "current teacher"))
                        {
                            lst.Add(new SelectListItem { Text = "Current teacher", Value = "Current teacher" });
                            lst.Add(new SelectListItem { Text = "Retired teacher", Value = "retired teacher" });
                        }
                        else
                        {
                            lst.Add(new SelectListItem { Text = "Retired teacher", Value = "retired teacher" });
                            lst.Add(new SelectListItem { Text = "Current teacher", Value = "Current teacher" });
                        }





                        ViewBag.status = lst;

                        int teacherID = Convert.ToInt32(TempData["teachid"]);
                        TeacherModel model1 = dal.viewTeacherProfile(teacherID);
                        ViewBag.id = model1.teacherID;
                        ViewBag.name = model1.teacherName;
                        ViewBag.designation = model1.teacherDesignation;
                        ViewBag.specialisation = model1.subjectSpecilization;
                        ViewBag.address = model1.teacherAddress;
                        ViewBag.mbl = model1.teacherMobileNo;
                        ViewBag.email = model1.teacherEmailID;

                        TempData["teachid"] = teacherID;
                        return View();
                    }
                imageAddress.SaveAs(Server.MapPath("~/TeacherImages/" + model.teacherID + Path.GetExtension(imageAddress.FileName)));
                model.imageAddress = "/TeacherImages/" + model.teacherID + Path.GetExtension(imageAddress.FileName);
                }
                int teacherID1 = Convert.ToInt32(TempData["teachid"]);

                if (dal.updateTeacherProfile(teacherID1, model))
                {
                    ViewBag.Redirectionmsg = "Teacher Updated Successfully.";
                    ViewBag.Btnlbl = "Updated Another Teacher";
                    ViewBag.BtnAction = "ShowTeachersList";
                    return View("Success");
                    //return View("TeacherUpdated");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Teacher not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "ShowTeachersList";
                    return View("Fail");
                    //return View("TeacherNotUpdated");
                }
           //}
           //catch (Exception)
           //{
           //    return View("Error");
           //}
            }




        public ActionResult Logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LoginAdmin", "AdminLogin");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }




    
        [HttpGet]
        public ActionResult AddRemoveSection()
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

            ViewBag.classes = list_classes;


            List<SelectListItem> list_sections = new List<SelectListItem>();
            list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
            list_sections.Add(new SelectListItem { Text = "1", Value = "1" });
            list_sections.Add(new SelectListItem { Text = "2", Value = "2" });
            list_sections.Add(new SelectListItem { Text = "3", Value = "3" });
            list_sections.Add(new SelectListItem { Text = "4", Value = "4" });

            ViewBag.sec = list_sections;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult GetNoofSectionsAjax(int enteredClass)
        {
            try
            { 
            TeacherDAL dal = new TeacherDAL();
            int noOfSec = dal.getSections(enteredClass);
            return Json(noOfSec, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult AddRemoveSection(AddRemoveSectionModel model)
        {
            try
            { 
            TeacherDAL dal = new TeacherDAL();

            List<SelectListItem> list_classes = new List<SelectListItem>();
            list_classes.Add(new SelectListItem { Text = "Select", Value = "" });
            list_classes.Add(new SelectListItem { Text = "class- V", Value = "5" });
            list_classes.Add(new SelectListItem { Text = "class- VI", Value = "6" });
            list_classes.Add(new SelectListItem { Text = "class- VII", Value = "7" });
            list_classes.Add(new SelectListItem { Text = "class- VIII", Value = "8" });
            list_classes.Add(new SelectListItem { Text = "class- IX", Value = "9" });
            list_classes.Add(new SelectListItem { Text = "class- X", Value = "10" });

            ViewBag.classes = list_classes;


            List<SelectListItem> list_sections = new List<SelectListItem>();
            list_sections.Add(new SelectListItem { Text = "Select", Value = "" });
            list_sections.Add(new SelectListItem { Text = "1", Value = "1" });
            list_sections.Add(new SelectListItem { Text = "2", Value = "2" });
            list_sections.Add(new SelectListItem { Text = "3", Value = "3" });
            list_sections.Add(new SelectListItem { Text = "4", Value = "4" });

            ViewBag.sec = list_sections;


            if (dal.updateNoOfSection(model))
            {
                ViewBag.msg = "No of sections updated successfully!";
                ModelState.Clear();
                return View();
            }
            else
            {
                ViewBag.ermsg = "No of sections Not Updated!";
                ModelState.Clear();
                return View();
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAdmin(AdminModel model, HttpPostedFileBase imageAddress)
        {
            try
            { 
            if (imageAddress != null)
            {
                if (!isValidContentProfilePic(imageAddress.ContentType))
                {
                    ViewBag.Error = "Only jpg and png are allowed";
                    return View();
                }

                if (imageAddress.ContentLength > 5000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 5mb";
                    return View();
                }
               
            }


            if (ModelState.IsValid)
            {
                string idpassword = dal.addAdmin(model,imageAddress);
                string[] arr = idpassword.Split(' ');


                ViewBag.id = arr[0];
                ViewBag.password = arr[1];

                if (imageAddress != null)
                {
                    imageAddress.SaveAs(Server.MapPath("~/AdminImages/" + model.adminID + Path.GetExtension(imageAddress.FileName)));
                }
                return View("AdminAdded");
            }
            else
            {
                return View("AdminNotAdded");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        ///update admin
        ///

        [HttpGet]
        public ActionResult UpdateAdminByID()
        {
            ViewBag.err = TempData["inv"];
            return View();
        }

        [HttpPost]
        public ActionResult UpdateAdminByID(UpdateAdminByIDModel model)
        {
            try
            {
          
            TempData["adminid"] = model.adminID;
            return RedirectToAction("UpdateAdminProfile");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult UpdateAdminProfile()
        {
            try
            {
                List<SelectListItem> list_status = new List<SelectListItem>();
            
            int adminID =Convert.ToInt32( TempData["adminid"]);

            if (adminID != 0)
            {
                UpdateAdminModel model = dal.viewAdminProfile(adminID);
                if (model.adminID == 0)
                {
                    TempData["inv"] = "Invalid Admin ID";
                    return RedirectToAction("UpdateAdminByID");
                }
                if(model.adminStatus=="Active")
                {
                   
                    list_status.Add(new SelectListItem { Text = "Active", Value = "Active" });
                    list_status.Add(new SelectListItem { Text = "Deactive", Value = "Deactive" });

                   
                }
                else
                {
                    list_status.Add(new SelectListItem { Text = "Deactive", Value = "Deactive" });
                    list_status.Add(new SelectListItem { Text = "Active", Value = "Active" });
                    

                }

                ViewBag.Astatus = list_status;

                ViewBag.AdminID = model.adminID;
                ViewBag.Name = model.adminName;

                TempData["adminid"] = adminID;
                return View();
            }
            else
            {
                return RedirectToAction("UpdateAdminByID");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UpdateAdminProfile(UpdateAdminModel model)
        {
            try
            {
                int adminID = Convert.ToInt32(TempData["adminid"]);
                if (dal.updateAdminProfile(adminID, model))
                {
                    ViewBag.Redirectionmsg = "Admin Updated Successfully.";
                    ViewBag.Btnlbl = "Update Another Admin";
                    ViewBag.BtnAction = "UpdateAdminByID";
                    return View("Success");
                    //return View("AdminUpdated");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Admin not Updated.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "UpdateAdminByID";
                    return View("Fail");
                    //return View("AdminNotUpdated");
                }
                // return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }





        [HttpGet]
        public ActionResult AddStudent()
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
        public ActionResult AddStudent(StudentModel studentModel)
        {
            try
            { 

            if (ModelState.IsValid)
            {
                AdminDAL tDAL = new AdminDAL();
                String id = tDAL.addStudent(studentModel);
                if (id != null)
                {
                    ViewBag.Redirectionmsg = "Student Added Successfully.";
                    ViewBag.Btnlbl = "Add Another Student";
                    ViewBag.BtnAction = "AddStudent";
                    return View("Success");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Student not Added.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "AddStudent";
                    return View("Fail");
                }
                    //return View("StudentNotAdded");

            }
            else
            {
                ViewBag.Redirectionmsg = "Student not Added.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AddStudent";
                return View("Fail");
                //return View("StudentNotAdded");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        

    
        [HttpGet]
        public ActionResult UpdateStudentProfile(String id)
        {
            try
            { 
            string studentID = id;
            //if (studentID != "")
            //{
                StudentModel model = new AdminDAL().ViewStudent(studentID);

                ViewBag.id = model.studentId;
                ViewBag.stdname = model.name;
                ViewBag.stdob = model.dob;
                ViewBag.studentGender = model.studentGender;
                ViewBag.stAddmissionYear = model.AddmissionYear;
                ViewBag.stfather_guardian_name = model.father_guardian_name;
                ViewBag.strelationWithGuadrian = model.relationWithGuadrian;
                ViewBag.stmother_name = model.mother_name;
                ViewBag.guardian_mob = model.guardian_mob_no;
                ViewBag.staddress = model.address;
                ViewBag.stclass_name = model.class_name;
                ViewBag.stresultStatus = model.resultStatus;
                ViewBag.ststudentStatus = model.studentStatus;
                ViewBag.stpassword = model.password;

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

                List<SelectListItem> result_Status = new List<SelectListItem>();

                if (model.resultStatus == "Pass")
                {
                    result_Status.Add(new SelectListItem { Text = "Pass", Value = "Pass" });
                    result_Status.Add(new SelectListItem { Text = "Fail", Value = "Fail" });
                }
                else
                {
                    result_Status.Add(new SelectListItem { Text = "Fail", Value = "Fail" });
                    result_Status.Add(new SelectListItem { Text = "Pass", Value = "Pass" });
                }
                

                List<SelectListItem> student_Status = new List<SelectListItem>();
                //student_Status.Add(new SelectListItem { Text = "Select", Value = "" });
                if (model.studentStatus == "current")
                {
                    student_Status.Add(new SelectListItem { Text = "Current", Value = "current" });
                    student_Status.Add(new SelectListItem { Text = "Passout", Value = "passout" });
                    student_Status.Add(new SelectListItem { Text = "Dropout", Value = "dropout" });
                }
                else if (model.studentStatus == "passout")
                {
                    student_Status.Add(new SelectListItem { Text = "Passout", Value = "passout" });
                    student_Status.Add(new SelectListItem { Text = "Current", Value = "current" });
                    student_Status.Add(new SelectListItem { Text = "Dropout", Value = "dropout" });
                }
                else
                {
                    student_Status.Add(new SelectListItem { Text = "Dropout", Value = "dropout" });
                    student_Status.Add(new SelectListItem { Text = "Current", Value = "current" });
                    student_Status.Add(new SelectListItem { Text = "Passout", Value = "passout" });
                }
                

                ViewBag.classSelection = list_classes;
                ViewBag.resultStatusSelection = result_Status;
                ViewBag.studentStatusSelection = student_Status;

                return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
           
        }


        [HttpPost]
        public ActionResult UpdateStudentProfile(StudentModel s_model)
        {
            try
            { 
                if (new AdminDAL().updateStudent(s_model))
                {
                    ViewBag.Redirectionmsg = "Student Updated Successfully.";
                    ViewBag.Btnlbl = "Update Another Student";
                    ViewBag.BtnAction = "GetAllStudentList";
                    return View("Success");
                }
                else
                {
                    ViewBag.Redirectionmsg = "Student not Updated .";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "GetAllStudentList";
                    return View("Fail");
                    //TempData["nmsg"] = "Student Not Updated";
                    //return View();
                }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }



        private bool isValidContentImage(String ContentType)
        {
            return ContentType.Equals("image/jpeg") || ContentType.Equals("image/jpg") || ContentType.Equals("image/png");
        }

        [HttpGet]
        public ActionResult UploadToppers()
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
            list_classes.Add(new SelectListItem { Text = "class- XII", Value = "12" });
           

            ViewBag.classSelection1 = list_classes;

            List<SelectListItem> list_Position = new List<SelectListItem>();
            list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
            list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
            list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
            list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });

            ViewBag.PositionSelection = list_Position;

            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult UploadToppers(UploadToppersModel m)
        {
         try
         { 
             if(m.File!=null)
             {
                 bool flag = isValidContentImage(m.File.ContentType);
                 if (!flag)
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

                     ViewBag.classSelection1 = list_classes;

                     List<SelectListItem> list_Position = new List<SelectListItem>();
                     list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
                     list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
                     list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
                     list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });

                     ViewBag.PositionSelection = list_Position;
                     ViewBag.Error = "Only jpg and png are allowed";
                     return View();

                 }

                 if (m.File.ContentLength > 5000000)
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

                     ViewBag.classSelection1 = list_classes;

                     List<SelectListItem> list_Position = new List<SelectListItem>();
                     list_Position.Add(new SelectListItem { Text = "Select", Value = "" });
                     list_Position.Add(new SelectListItem { Text = "1st", Value = "1" });
                     list_Position.Add(new SelectListItem { Text = "2nd", Value = "2" });
                     list_Position.Add(new SelectListItem { Text = "3rd", Value = "3" });

                     ViewBag.PositionSelection = list_Position;


                     ViewBag.Error = "File Size Must Be Less Than 5mb";
                     return View();
                 }
             }
           

         
                AdminDAL Ad = new AdminDAL();
                String filename = Ad.UploadToppers(m);
                if (filename != "null")
                {
                    var path = Path.Combine(Server.MapPath("~/ToppersPic/"), filename);
                    m.File.SaveAs(path);
                }
                
                ViewBag.Redirectionmsg = "Topper Added Successfully.";
                ViewBag.Btnlbl = "Add Another Topper";
                ViewBag.BtnAction = "UploadToppers";
                return View("Success");

         }
         catch (Exception)
         {
             return View("Error");
         }

        }
     




        public ActionResult ChangeAcademicYear()
        {
            try
            { 
           List<AdminWhoChangedAcademicYearModel> lst_admins= dal.ShowAdminsWhoChangedAcademicYear();
            if (TempData["Msg"] == null)
            {
                if (TempData["errMsg"] == null)
                {
                    ViewBag.msg = "";
                    ViewBag.emsg = "";
                    return View(lst_admins);
                }
                else
                {
                    ViewBag.msg = "";
                    ViewBag.emsg = TempData["errMsg"];
                    return View(lst_admins);
                }
            }
            else
            {
                if (TempData["errMsg"] == null)
                {
                    ViewBag.msg = TempData["Msg"];
                    ViewBag.emsg = "";
                    return View(lst_admins);
                }
                else
                {
                    ViewBag.msg = TempData["Msg"];
                    ViewBag.emsg = TempData["errMsg"];
                    return View(lst_admins);
                }
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult Increamentyear(string action)
        {
            try
            { 
            AdminDAL Dal = new AdminDAL();
            bool flag = Dal.IncrementYear(Convert.ToInt32(User.Identity.Name));
            if (flag)
            {
                TempData["Msg"] = "Academic Year Updated Successfully.";
                return RedirectToAction("ChangeAcademicYear");
            }
            else
            {
                TempData["errMsg"] = "Something went Wrong ,Try Again.";
                return View("ChangeAcademicYear");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        [HttpGet]
        public ActionResult Decreamentyear(string action1)
        {
            try
            { 
            AdminDAL Dal = new AdminDAL();
            bool flag = Dal.DecrementYear(Convert.ToInt32(User.Identity.Name));
            if (flag)
            {
                TempData["Msg"] = "Academic Year Updated Successfully.";
                return RedirectToAction("ChangeAcademicYear");
            }
            else
            {
                TempData["errMsg"] = "Something went Wrong ,Try Again.";
                return RedirectToAction("ChangeAcademicYear");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }




        ///////////////////////////////////////
        [HttpGet]
        public ActionResult ChangeResultStatusToFail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangeResultStatusToFail(ChangeStudentResultSt CSRS)
        {
            try
            { 
            if (CSRS.StudentId.Length == 8 && (CSRS.StudentId[0] == 's' || CSRS.StudentId[0] == 'S'))
            {
                AdminDAL D = new AdminDAL();
                if (D.ChangeResultStatus(CSRS))
                    ViewBag.msg = "Student Result Status Successfully changed To " + CSRS.Status;
                else
                    ViewBag.emsg = "No student found";
                return View();
            }
            else
            {
                ViewBag.emsg = "Invalid Student Id";
                return View();
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult ShowTeachersList()
        {
            try
            { 
             List<GetAllTeacherModel> list_allTeachers = dal.getAllTeachers();
             return View(list_allTeachers);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        ////////////911/////////////////////////////
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
            int OfCLass = Convert.ToInt32(TempData["StdClass"]);
            ViewBag.Class = OfCLass.ToString();
            if (OfCLass != 0)
            {
                List<StudentDataFetchModel> obj = dal.GetAllstudent(OfCLass);
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






        ////////////////////////////////Update password 09/11 /////////////////////////////////////////////////////
        [HttpGet]
        public ActionResult UpdatePasswordAdmin()
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
        public ActionResult UpdatePasswordAdmin(ChangePasswordAdminModel model)
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


                int adminID = Convert.ToInt32(User.Identity.Name);
                //LoginDAL dal = new LoginDAL();
                if (dal.updatePassword(adminID, model))
                {
                    TempData["updatedmsg"] = "Password is updated";
                    return RedirectToAction("Index");
                }
                else
                {
                   
                    ViewBag.Error = "Please check your old password again!";
                    return View();
                }
                //return View("NotUpdated");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        ///////For Fetching passout or 10th pass Left Students///////////////
        public ActionResult Students()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetLeftStudendts()
        {
            try
            { 
            List<SelectListItem> lst_ClassofPass = new List<SelectListItem>();
            lst_ClassofPass.Add(new SelectListItem { Text = "Select", Value = "" });
            lst_ClassofPass.Add(new SelectListItem { Text = "10", Value = "10th pass" });
            lst_ClassofPass.Add(new SelectListItem { Text = "12", Value = "passout" });
            ViewBag.ClassofPass = lst_ClassofPass;

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            int y = dt.Year;
            List<SelectListItem> lst_PassOutYear = new List<SelectListItem>();
            lst_PassOutYear.Add(new SelectListItem { Text = "Select", Value = "" });
            for (int i = 2010; i <= y; i++)
            {
                lst_PassOutYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }


            ViewBag.Yearofpass = lst_PassOutYear;
            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult GetLeftStudendts(ShowAllPassoutStudentModel model)
        {
            try
            { 
            TempData["Class_name"] = model.OfClass;
            TempData["year12"] = model.Year;
            return RedirectToAction("AllPassOutStudent");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult AllPassOutStudent()
        {
            try
            { 
            if (TempData["Class_name"] == null)
            {
                return RedirectToAction("GetLeftStudendts");
            }

            String ofClass = TempData["Class_name"].ToString();
            String Year = TempData["year12"].ToString();
            ViewBag.Yearofpassout = Year;
            List<StudentModel> LSM = dal.ViewLeftStudent(Year, ofClass);
            return View(LSM);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        /////////////////////////////////subha 12/11/17///////////////////////
        [HttpGet]
        public ActionResult AddAlumni()
        {
            try
            { 
            List<SelectListItem> list_alumniClass = new List<SelectListItem>();
            list_alumniClass.Add(new SelectListItem { Text = "Select", Value = "" });
            list_alumniClass.Add(new SelectListItem { Text = "10", Value = "10" });
            list_alumniClass.Add(new SelectListItem { Text = "12", Value = "12" });
            ViewBag.alumniClass = list_alumniClass;

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            int y = dt.Year;
            List<SelectListItem> list_alumniYear = new List<SelectListItem>();
            list_alumniYear.Add(new SelectListItem { Text = "Select", Value = "" });
            for (int i = 1957; i <= y; i++)
            {
                list_alumniYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.alumniYear = list_alumniYear;

            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AddAlumni(AlumniModel a_model)
        {
            try
            { 
            if (dal.AddAlumni(a_model))
            {
                ViewBag.Redirectionmsg = "School Topper Added Successfully.";
                ViewBag.Btnlbl = "Add Another School Topper";
                ViewBag.BtnAction = "AddAlumni";
                return View("Success");
            }
            else
            {
                ViewBag.Redirectionmsg = "School Topper not Added.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AddAlumni";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpGet]
        public ActionResult AlumniUpdateTable()
        {
            try
            { 
            List<AlumniModel> list_allAlumni = dal.GetAllAlumni();
            if (!list_allAlumni.Exists(x => x.allumniClass == "10"))
            {
                ViewBag.blank10 = "No data added";
                if (!list_allAlumni.Exists(x => x.allumniClass == "12"))
                {
                    ViewBag.blank12 = "No data added";
                    return View(list_allAlumni);
                }
                return View(list_allAlumni);
            }
            else
            {
                if (!list_allAlumni.Exists(x => x.allumniClass == "12"))
                {
                    ViewBag.blank12 = "No data added";
                    return View(list_allAlumni);
                }
                return View(list_allAlumni);
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AlumniUpdate(int id)
        {
            try
            { 
            if (id == 0 || id.ToString() == "")
            {
                return RedirectToAction("AlumniUpdateTable");
            }
            AlumniModel a_model = dal.GetAlumniByID(id);
            if (a_model.alumniID == 0)
            {
                return RedirectToAction("AlumniUpdateTable");
            }
            ViewBag.aid = a_model.alumniID;
            ViewBag.aName = a_model.name;
            ViewBag.aClass = a_model.allumniClass;
            ViewBag.aYear = a_model.year;

            List<SelectListItem> list_alumniClass = new List<SelectListItem>();
            list_alumniClass.Add(new SelectListItem { Text = "Select", Value = "" });
            list_alumniClass.Add(new SelectListItem { Text = "10", Value = "10" });
            list_alumniClass.Add(new SelectListItem { Text = "12", Value = "12" });
            ViewBag.alumniClass = list_alumniClass;

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            int y = dt.Year;
            List<SelectListItem> list_alumniYear = new List<SelectListItem>();
            list_alumniYear.Add(new SelectListItem { Text = "Select", Value = "" });
            for (int i = 1957; i <= y; i++)
            {
                list_alumniYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.alumniYear = list_alumniYear;

            return View(a_model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AlumniUpdate(AlumniModel a_model)
        {
            try
            { 
            if (dal.UpdateAlumni(a_model))
            {
                ViewBag.Redirectionmsg = "School Topper Updated Successfully.";
                ViewBag.Btnlbl = "Update Another School Topper";
                ViewBag.BtnAction = "AlumniUpdateTable";
                return View("Success");
            }
            else
            {
                ViewBag.Redirectionmsg = "School Topper not Updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "AlumniUpdateTable";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        //////////////////// souvik 13/11 ////////////////
        [HttpGet]
        public ActionResult ShowAllFeedback()
        {
            try
            {
                List<FeedbackModel> list_fdbk = dal.showAllFeedback();
               // ViewBag.delerrmsg = TempData["FeedErrMsg"];
               // ViewBag.delmsg = TempData["FeedMsg"];
                return View(list_fdbk);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        ///////////////////////////subha 14/11////////////
        [HttpGet]
        public ActionResult Addoc()
        {
            try
            { 
            List<SelectListItem> list_ocCategory = new List<SelectListItem>();
            list_ocCategory.Add(new SelectListItem { Text = "Select", Value = "" });
            list_ocCategory.Add(new SelectListItem { Text = "President", Value = "President" });
            list_ocCategory.Add(new SelectListItem { Text = "Secretary", Value = "Secretary" });
            ViewBag.ocCategory = list_ocCategory;

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            int y = dt.Year;
            List<SelectListItem> list_ocYear = new List<SelectListItem>();
            list_ocYear.Add(new SelectListItem { Text = "Select", Value = "" });
            for (int i = 1949; i <= y; i++)
            {
                list_ocYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.ocYear = list_ocYear;

            return View();
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public ActionResult Addoc(OCModel oc_model)
        {
            try
            { 
            if (dal.AddOC(oc_model))
            {
                ViewBag.Redirectionmsg = "President/Secretary Added Successfully.";
                ViewBag.Btnlbl = "Add Another President/Secretary";
                ViewBag.BtnAction = "Addoc";
                return View("Success");
            }
            else
            {
                ViewBag.Redirectionmsg = "President/Secretary not Added.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "Addoc";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ocUpdateTable()
        {
            try
            { 
            List<OCModel> list_allOC = dal.GetAllOC();
            if (!list_allOC.Exists(x => x.ocPosition == "President"))
            {
                ViewBag.blankP = "No data added";
                if (!list_allOC.Exists(x => x.ocPosition == "Secretary"))
                {
                    ViewBag.blankS = "No data added";
                    return View(list_allOC);
                }
                return View(list_allOC);
            }
            else
            {
                if (!list_allOC.Exists(x => x.ocPosition == "Secretary"))
                {
                    ViewBag.blankS = "No data added";
                    return View(list_allOC);
                }
                return View(list_allOC);
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult OCUpdate(int id)
        {
            try
            { 
            if (id == 0 || id.ToString() == "")
            {
                return RedirectToAction("ocUpdateTable");
            }
            OCModel oc_model = dal.GetOCByID(id);
            if (oc_model.ocID == 0)
            {
                return RedirectToAction("ocUpdateTable");
            }
            ViewBag.ocID = oc_model.ocID;
            ViewBag.name = oc_model.ocName;
            ViewBag.ocPos = oc_model.ocPosition;
            ViewBag.OCfromYear = oc_model.fromYear;
            ViewBag.OCtoYear = oc_model.toYear;

            List<SelectListItem> list_ocCategory = new List<SelectListItem>();
            list_ocCategory.Add(new SelectListItem { Text = "Select", Value = "" });
            list_ocCategory.Add(new SelectListItem { Text = "President", Value = "President" });
            list_ocCategory.Add(new SelectListItem { Text = "Secretary", Value = "Secretary" });
            ViewBag.ocCategory = list_ocCategory;

            DateTime dt = Convert.ToDateTime(DateTime.Now.ToString());
            int y = dt.Year;
            List<SelectListItem> list_ocYear = new List<SelectListItem>();
            list_ocYear.Add(new SelectListItem { Text = "Select", Value = "" });
            for (int i = 1949; i <= y; i++)
            {
                list_ocYear.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            ViewBag.ocYear = list_ocYear;

            return View(oc_model);
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult OCUpdate(OCModel oc)
        {
            try
            { 
            if (dal.UpdateOC(oc))
            {
                ViewBag.Redirectionmsg = "President/Secretary Updated Successfully.";
                ViewBag.Btnlbl = "Update Another President/Secretary";
                ViewBag.BtnAction = "ocUpdateTable";
                return View("Success");
            }
            else
            {
                ViewBag.Redirectionmsg = "President/Secretary not Updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "ocUpdateTable";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }


        public ActionResult FailStudent(string id)
        {
            try
            { 
            ChangeStudentResultSt Csr = new ChangeStudentResultSt();
            Csr.StudentId = id;
            Csr.Status = "Fail";
            if (dal.ChangeResultStatus(Csr))
            {
                int ofclass = dal.FindClass(id);
                TempData["StdClass"] = ofclass;
                return RedirectToAction("AllStudent");
               
            }
            else
            {

                ViewBag.Redirectionmsg = "Status Not Updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "GetAllStudentList";
                return View("Fail");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }

        }
        public ActionResult PassStudent(string id)
        {
            try
            { 
            ChangeStudentResultSt Csr = new ChangeStudentResultSt();
            Csr.StudentId = id;
            Csr.Status = "Pass";
            if (dal.ChangeResultStatus(Csr))
            {
                int ofclass = dal.FindClass(id);
                TempData["StdClass"] = ofclass;
                return RedirectToAction("AllStudent");
            }
            else
            {

                ViewBag.Redirectionmsg = "Status Not Updated.";
                ViewBag.Btnlbl = "Try Again";
                ViewBag.BtnAction = "GetAllStudentList";
                return View("Fail");
            }
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


        [HttpGet]
        public ActionResult RoutineUpload()
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
        [HttpPost]
        public ActionResult RoutineUpload(RoutineModel r)
        {
            try
            {
                // ModelState.Clear();
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


                bool flag = isValidContentNotice(r.File.ContentType);
                if (!flag)
                {
                    ViewBag.Error = "Only pdf,jpg,docx,doc,xlns are allowed";
                    return View();
                }

                if (r.File.ContentLength > 5000000)
                {
                    ViewBag.Error = "File Size Must Be Less Than 5mb";
                    return View();
                }

                TeacherDAL dal = new TeacherDAL();
                int noOfSec = dal.getSections(r.RoutineForClass);

                if (r.RoutineForClass != 111 && r.RoutineForClass != 112 && r.RoutineForClass != 113 && r.RoutineForClass != 121 && r.RoutineForClass != 122 && r.RoutineForClass != 123)
                {
                    if (r.RoutineForSection == "B" && noOfSec < 2 || r.RoutineForSection == "C" && noOfSec < 3 || r.RoutineForSection == "D" && noOfSec < 4 || r.RoutineForSection == "Science" || r.RoutineForSection == "Arts" || r.RoutineForSection == "Commerce")
                    {
                        ViewBag.errmsg = "Invalid Section";
                        return View();
                    }
                }

                if (r.RoutineForClass == 111 || r.RoutineForClass == 112 || r.RoutineForClass == 113 || r.RoutineForClass == 121 || r.RoutineForClass == 122 || r.RoutineForClass == 123)
                {
                    if (r.RoutineForSection == "A" || r.RoutineForSection == "B" || r.RoutineForSection == "C" || r.RoutineForSection == "D")
                    {
                        ViewBag.errmsg = "Invalid Section";
                        return View();
                    }

                    if ((r.RoutineForClass == 111 && r.RoutineForSection != "Science") || (r.RoutineForClass == 112 && r.RoutineForSection != "Arts") || (r.RoutineForClass == 113 && r.RoutineForSection != "Commerce") || (r.RoutineForClass == 121 && r.RoutineForSection != "Science") || (r.RoutineForClass == 122 && r.RoutineForSection != "Arts") || (r.RoutineForClass == 123 && r.RoutineForSection != "Commerce"))
                    {
                        ViewBag.errmsg = "Invalid Section";
                        return View();
                    }
                }




                int id = Convert.ToInt32(User.Identity.Name);
                TeacherDAL tDal = new TeacherDAL();
                var Filename = tDal.UploadRoutine(r, id);
                if (Filename.Equals("fail"))
                {
                    ViewBag.Redirectionmsg = "Routine was not Uploaded.";
                    ViewBag.Btnlbl = "Try Again";
                    ViewBag.BtnAction = "RoutineUpload";
                    return View("Fail");
                    //ViewBag.Error = "";
                    //return View();
                }
                else
                {
                    var path = Path.Combine(Server.MapPath("~/Routines/"), Filename);
                    r.File.SaveAs(path);
                    ViewBag.Redirectionmsg = "Routine Uploaded Successfully.";
                    ViewBag.Btnlbl = "Upload Another Routine";
                    ViewBag.BtnAction = "RoutineUpload";
                    return View("Success");
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
         
            List<ViewGalleryModel> obj = dal.getAllPhotos();
            ViewBag.Msg = TempData["PhotoDelmsg"];
            ViewBag.eMsg = TempData["PhotoDelemsg"];
            return View(obj);
        }

        public ActionResult DeletePhotos(int id)
        {
          
            string path = dal.getImageAddress(id);

            string Path = Request.MapPath("~" + path);

            if (dal.deletePhoto(id))
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



        ///news
        [HttpGet]
        public ActionResult EventsUpdateTable()
        {
            try
            {
                //TeacherDAL tDAL = new TeacherDAL();
                List<NewsModel> list_events = dal.GetAllEvents();

                if (list_events.Count == 0)
                {
                    ViewBag.noEventsFound = "No Events Found";
                    ViewBag.dlt = TempData["dlt"];
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


        public ActionResult DeleteNews(int newsID)
        {
            try
            {
                //TeacherDAL tDAL = new TeacherDAL();
                int count = dal.DeleteNews(newsID);

                if (count == 1)
                {
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
        //////////////////////////////////////////subha 21/11/2017///NoticeUpload shift from teacherController

        [HttpGet]
        public ActionResult NoticeUpload()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult NoticeUpload(NoticeModel n)
        {
            try
            {
            // ModelState.Clear();
            bool flag = isValidContentNotice(n.NoticePath.ContentType);
            if (!flag)
            {
                ViewBag.Error = "Only pdf,jpg,docx,doc,xlns are allowed";
                return View();
            }
            if (n.NoticePath.ContentLength > 5000000)
            {
                ViewBag.Error = "File Size Must Be Less Than 5mb";
                return View();
            }

            int id = Convert.ToInt32(User.Identity.Name);
            TeacherDAL t = new TeacherDAL();
            var Filename = t.UploadNotice(n, id);
            var path = Path.Combine(Server.MapPath("~/Notices/"), Filename);
            n.NoticePath.SaveAs(path);
            ViewBag.Redirectionmsg = "Notice Uploaded Successfully.";
            ViewBag.Btnlbl = "Upload Another Notice";
            ViewBag.BtnAction = "NoticeUpload";
            return View("Success");
            }
            catch (Exception)
            {
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult ViewAllNotices()
        {
            try{
            TeacherDAL tDAL = new TeacherDAL();
            //notice upload was first for a teacher after it was chnaged to admin but the dal function was not
            //chnaged from teacherDAL to AdminDAL..the upload and delete dal methods are in TeacherDAL


            List<NoticeModel> list_allNotices = tDAL.getAllNotices();
            if (list_allNotices.Count == 0)
            {
                ViewBag.noNotice = "No Notices Found";
                ViewBag.msg = TempData["Notice"];
                return View(list_allNotices);
            }
            else
            {
                ViewBag.msg = TempData["Notice"];
                ViewBag.errmsg = TempData["NoticeErr"];
                return View(list_allNotices);
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult DeleteNotice(int id)
        {
            try{
            //notice upload was first for a teacher after it was chnaged to admin but the dal function was not
            //chnaged from teacherDAL to AdminDAL..the upload and delete dal methods are in TeacherDAL

            TeacherDAL tDAL = new TeacherDAL();
            string img = tDAL.getNoticeImgPath(id);
            string path = Request.MapPath("~" + img);

            if (tDAL.deleteNotice(id))
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                TempData["Notice"] = "Notice Deleted Successfully.";
                return RedirectToAction("ViewAllNotices");
            }
            else
            {
                TempData["NoticeErr"] = "Notice was not Deleted, Try Again.";
                return RedirectToAction("ViewAllNotices");
            }
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}
