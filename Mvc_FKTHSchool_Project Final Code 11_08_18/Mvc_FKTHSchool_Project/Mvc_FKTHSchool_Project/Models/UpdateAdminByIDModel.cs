
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateAdminByIDModel
    {
        [Display(Name = "Enter Admin ID")]
        //[StringLength(2, ErrorMessage = "Max 2 characters")]
        //[RegularExpression("([Ss][0-9]*)", ErrorMessage = "Student ID should start with 'S'/'s'")]
        [Required(ErrorMessage = "Please enter Admin ID")]
        public int adminID { get; set; }
    }
}