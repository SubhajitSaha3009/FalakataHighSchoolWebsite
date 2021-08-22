using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class GalleryUploadModel
    {
        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int TeacherId { get; set; }

        [Display(Name = "Image Title")]
        [StringLength(30, ErrorMessage = "Max 30 characters")]
        [Required(ErrorMessage = "*")]
        public string title { get; set; }

        [Display(Name = "Image Description")]
        [StringLength(125, ErrorMessage = "Max 125 characters")]
        [Required(ErrorMessage = "*")]
        public string imageDescription { get; set; }

        [Display(Name = "Upload Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }
    }
}