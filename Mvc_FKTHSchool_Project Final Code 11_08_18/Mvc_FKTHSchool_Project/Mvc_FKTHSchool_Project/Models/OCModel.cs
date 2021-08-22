using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class OCModel
    {
        public int ocID { get; set; }

        [Display(Name = "Name")]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        [Required(ErrorMessage = "*")]
        public string ocName { get; set; }

        [Display(Name = "Position")]
        [Required(ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Max 10 chars")]
        public string ocPosition { get; set; }

        [Display(Name = "From Year")]
        [Required(ErrorMessage = "*")]
        [StringLength(4, ErrorMessage = "Max 4 chars")]
        public string fromYear { get; set; }

        [Display(Name = "To Year")]
        [StringLength(4, ErrorMessage = "Max 4 chars")]
        public string toYear { get; set; }
    }
}