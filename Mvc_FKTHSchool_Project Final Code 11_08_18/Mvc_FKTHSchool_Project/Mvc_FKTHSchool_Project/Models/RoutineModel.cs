using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class RoutineModel
    {
        //[Display(Name = "Routine ID")]
        //[Required(ErrorMessage = "*")]
        //public int RoutineId { get; set; }

        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int TeacherId { get; set; }


        //[Display(Name = "Teacher Name")]
        //[Required(ErrorMessage = "*")]
        //public string TeacherName { get; set; }

        [Display(Name = "Routine for Class")]
        [Required(ErrorMessage = "*")]
        public int RoutineForClass { get; set; }

        [Display(Name = "Routine for Section")]
        [Required(ErrorMessage = "*")]
        public string RoutineForSection { get; set; }

        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase File { get; set; }

      

        
    }
}