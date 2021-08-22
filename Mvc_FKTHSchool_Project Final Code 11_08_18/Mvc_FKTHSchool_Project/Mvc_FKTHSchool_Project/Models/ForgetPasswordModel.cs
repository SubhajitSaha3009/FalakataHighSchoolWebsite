using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class ForgetPasswordModel
    {
       
            [Display(Name = "Enter Login ID")]
            [Required(ErrorMessage = "*")]
            public int loginID { get; set; }

            [Display(Name = "Select Secuity Question")]
            [Required(ErrorMessage = ("*"))]
            [StringLength(100, ErrorMessage = "Max 100 chars")]
            public string securityQuestion { get; set; }


            [Display(Name = "Enter Security Answer")]
            [Required(ErrorMessage = ("*"))]
            [StringLength(30, ErrorMessage = "Max 30 chars")]
            public string securityAnswer { get; set; }
        
    }
}