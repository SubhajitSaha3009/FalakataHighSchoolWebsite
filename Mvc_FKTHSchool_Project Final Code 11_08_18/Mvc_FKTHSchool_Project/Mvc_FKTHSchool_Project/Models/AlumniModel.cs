using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class AlumniModel
    {
        public int alumniID { get; set; }

        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        [Required(ErrorMessage = "*")]
        public string name { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "*")]
        public string allumniClass { get; set; }

        [Display(Name = "Year")]
        [Required(ErrorMessage = "*")]
        public string year { get; set; }
    }
}