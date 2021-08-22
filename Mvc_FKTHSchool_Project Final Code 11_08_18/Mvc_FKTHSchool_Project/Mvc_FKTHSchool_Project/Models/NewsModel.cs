using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class NewsModel
    {
        [Display(Name = "News ID")]
        [Required(ErrorMessage = "*")]
        public int newsID { get; set; }

        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int teacherID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Max 30 characters")]
        public string newsTitle { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "*")]
        [StringLength(300, ErrorMessage = "Max 300 characters")]
        public string newsDescription { get; set; }

        [Display(Name = "Event's Date")]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        [Required(ErrorMessage = "*")]
        public string newsDate { get; set; }


        public string uploadDate { get; set; }


        [Display(Name = "Upload Image")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

        [StringLength(100, ErrorMessage = "Max 100 characters")]
        public string newsImageAddress { get; set; }

    }
}