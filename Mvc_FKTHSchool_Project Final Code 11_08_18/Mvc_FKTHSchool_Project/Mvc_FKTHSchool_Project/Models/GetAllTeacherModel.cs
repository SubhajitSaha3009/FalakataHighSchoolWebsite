using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class GetAllTeacherModel
    {
        [Display(Name = "Teacher ID")]
        public int teacherID { get; set; }

        [Display(Name = "Teacher Name")]
        public string teacherName { get; set; }

        [Display(Name = "Teacher Designation")]
        public string teacherDesignation { get; set; }

        [Display(Name = "Teacher Specialization")]
        public string subjectSpecilization { get; set; }

        public string Qualification { get; set; }

        public string imageAddress { get; set; }

        public string teacherGender { get; set; }

        public string teacherStatus { get; set; }
    }
}