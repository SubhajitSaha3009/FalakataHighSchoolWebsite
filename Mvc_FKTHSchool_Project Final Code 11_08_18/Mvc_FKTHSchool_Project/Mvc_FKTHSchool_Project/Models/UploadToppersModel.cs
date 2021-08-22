using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UploadToppersModel
    {
        [Display(Name = "Select Class")]
        [Required(ErrorMessage = "*")]
        public int OfClass { get; set; }

        [Display(Name = "Select Position")]
        [Required(ErrorMessage = "*")]
        public int Position { get; set; }

        [Display(Name = "Enter Student name")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Max 30 Character")]
        public String StdName { get; set; }


        [Display(Name = "Upload Image")]
        //[Required(ErrorMessage = "*")]
        public HttpPostedFileBase File { get; set; }
    }
}