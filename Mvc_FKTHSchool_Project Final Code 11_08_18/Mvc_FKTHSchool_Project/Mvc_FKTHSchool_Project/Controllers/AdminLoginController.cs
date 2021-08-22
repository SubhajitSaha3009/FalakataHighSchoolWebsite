using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mvc_FKTHSchool_Project.Models;

namespace Mvc_FKTHSchool_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminLoginController : Controller
    {
        //
        // GET: /AdminLogin/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogoutAdmin()
        {
            try
            {
                FormsAuthentication.SignOut();
                return RedirectToAction("LoginAdmin", "AdminLogin");
            }
            catch(Exception)
            {
                return View("Error");
            }
        }


        [AllowAnonymous]
        [HttpGet]
        public ActionResult LoginAdmin()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public ActionResult LoginAdmin(LoginAdminModel model)
        {
            try
            {
                LoginDAL dal = new LoginDAL();


                if (model.LogInId.ToString().Length == 2)
                {
                    CheckPasswordStatusEnum status = (CheckPasswordStatusEnum)dal.loginAdmin(model);
                    if (status == CheckPasswordStatusEnum.WrongPassword)
                    {
                        ViewBag.errmsg = "Invalid login ID or password!";
                        return View();
                    }
                    else if (status == CheckPasswordStatusEnum.NewUser)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("UpdatePasswordAdmin");
                    }
                    else if (status == CheckPasswordStatusEnum.Updated)
                    {
                        FormsAuthentication.SetAuthCookie(model.LogInId.ToString(), false);
                        return RedirectToAction("Index", "Admin");
                    }
                }
                ViewBag.errmsg = "Enter Valid Login ID";
                return View();
            }
            catch(Exception)
            {
                return View("Error");
            }
           
        }

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
        public ActionResult UpdatePasswordAdmin(UpdatePasswordAdminModel model)
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


            int adminid = Convert.ToInt32(User.Identity.Name);
            LoginDAL dal = new LoginDAL();
            if (dal.updatePasswordAdmin(adminid, model))
            {
                return View("UpdatedPasswordAdmin");
            }
            return View("NotUpdatedPasswordAdmin");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            try
            { 
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginAdmin");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }





        ///////////////////////////////////////// souvik 10/11 forget password ///////////////////////////
        [AllowAnonymous]
        [HttpGet]
        public ActionResult ForgetPasswordAdmin() // admin
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

        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgetPasswordAdmin(ForgetPasswordModel model)
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
       

    }
}
