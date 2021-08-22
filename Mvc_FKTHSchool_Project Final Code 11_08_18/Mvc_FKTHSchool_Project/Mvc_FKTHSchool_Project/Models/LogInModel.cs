using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class LogInModel
    {
        [Display(Name = "LogIn ID")]
        [Required(ErrorMessage = "*")]
        public String LogInId { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        public String Password { get; set; }

        //[Display(Name = "DOB")]
        //[Required(ErrorMessage = "*")]
        //public String DOB { get; set; }

        [Display(Name = "Login As")]
        [Required(ErrorMessage = "*Select Catagoty")]
        public String category { get; set; }
    }
}