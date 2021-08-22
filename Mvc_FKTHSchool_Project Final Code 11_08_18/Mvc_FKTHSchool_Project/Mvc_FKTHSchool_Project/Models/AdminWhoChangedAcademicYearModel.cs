using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class AdminWhoChangedAcademicYearModel
    {

        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int adminID { get; set; }

        [Display(Name = "Action Taken")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 chars")]
        public string adminDone { get; set; }


        [Display(Name = "Date and Time")]
        [Required(ErrorMessage = "*")]
        //[StringLength(60, ErrorMessage = "Max 60 chars")]
        public string dateAndTime { get; set; }
    }
}