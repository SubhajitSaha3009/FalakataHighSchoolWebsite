using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateAdminModel
    {
        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int adminID { get; set; }



        [Display(Name = "Admin Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(60, ErrorMessage = "Max 60 chars")]
        public string adminName { get; set; }



        [Display(Name = "Select Admin Status")]
        [Required(ErrorMessage = "*")]
        [StringLength(15, ErrorMessage = "Max 15 chars")]
        public string adminStatus { get; set; }
    }
}