using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class AdminModel
    {
        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int adminID { get; set; }



        [Display(Name = "Admin Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(60, ErrorMessage = "Max 60 chars")]
        public string adminName { get; set; }



        [Display(Name = "Admin Gender")]
        [Required(ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Max 10 chars")]
        public string adminGender { get; set; }



        [Display(Name = "Admin Designation")]
        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "Max 40 chars")]
        public string adminDesignation { get; set; }



        [Display(Name = "Upload passport size photo")]
        //[Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 chars")]
        public string imageAddress { get; set; }

    }
}