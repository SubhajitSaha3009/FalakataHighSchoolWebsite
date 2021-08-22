using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class NewsUploadModel
    {

        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int TeacherID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Max 30 characters")]
        public string newsTitle { get; set; }
        
        [Display(Name = "Description")]
        [Required(ErrorMessage = "*")]
        [StringLength(300, ErrorMessage = "Max 300 characters")]
        public string newsDescription { get; set; }

        [Display(Name = "Event's Date")]
        [Required(ErrorMessage = "*")]
        public string newsDate { get; set; }

        [Display(Name = "Upload Image")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public HttpPostedFileBase file { get; set; }
    }
}