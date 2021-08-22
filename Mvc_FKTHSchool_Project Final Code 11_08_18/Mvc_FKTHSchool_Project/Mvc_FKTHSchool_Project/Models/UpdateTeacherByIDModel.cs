using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateTeacherByIDModel
    {
        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "Enter TeacherID First")]
        public int teacherID { get; set; }
    }
}