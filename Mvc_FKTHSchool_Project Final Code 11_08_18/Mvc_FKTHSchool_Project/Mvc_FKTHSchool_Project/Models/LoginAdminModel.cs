using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class LoginAdminModel
    {

        [Display(Name = "LogIn ID")]
        [Required(ErrorMessage = "*")]
        public String LogInId { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        public String Password { get; set; }
    }
}