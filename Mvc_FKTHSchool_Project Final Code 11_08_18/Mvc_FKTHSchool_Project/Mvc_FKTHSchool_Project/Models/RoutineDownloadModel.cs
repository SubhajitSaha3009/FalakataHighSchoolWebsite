using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class RoutineDownloadModel
    {
        // [Display(Name = "Routine for Class")]
        //[Required(ErrorMessage = "*")]
        //public string RoutineForClass { get; set; }

         [Display(Name = "Routine for Section")]
         [Required(ErrorMessage = "*")]
         public string RoutineForSection { get; set; }

    }
}