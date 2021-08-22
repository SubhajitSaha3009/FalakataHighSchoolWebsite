using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class ShowAllPassoutStudentModel
    {
        [Display(Name = "Select Class")]
        [Required(ErrorMessage = "*")]
        public String OfClass { get; set; }

        [Display(Name = "Select Passout Year")]
        [Required(ErrorMessage = "*")]
        public String Year { get; set; }
    }
}