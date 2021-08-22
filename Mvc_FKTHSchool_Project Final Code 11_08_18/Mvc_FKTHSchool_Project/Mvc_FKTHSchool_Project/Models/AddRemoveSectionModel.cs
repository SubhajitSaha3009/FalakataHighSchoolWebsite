using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class AddRemoveSectionModel
    {
        [Display(Name = "Select Class")]
        [Required(ErrorMessage = "*")]
        public int enteredClass { get; set; }


        [Display(Name = "No of Section")]
        //[Required(ErrorMessage = "*")]
        public int enteredSection { get; set; }


        [Display(Name = "Change No of Section To")]
        [Required(ErrorMessage = "*")]
        public int modifiedSection { get; set; }
    }
}