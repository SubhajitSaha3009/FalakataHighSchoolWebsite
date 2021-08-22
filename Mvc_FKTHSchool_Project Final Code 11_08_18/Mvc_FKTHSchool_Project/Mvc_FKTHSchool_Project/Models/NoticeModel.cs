using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class NoticeModel
    {
        [Display(Name = "Notice ID")]
        [Required(ErrorMessage = "*")]
        public int NoticeId { get; set; }

        [Display(Name = "Admin ID")]
        [Required(ErrorMessage = "*")]
        public int AdminId { get; set; }

        [Display(Name = "Enter Notice Title")]
        [Required(ErrorMessage = "*")]
        [StringLength(80, ErrorMessage = "Max 80 chars")]
        public string NoticeTitle { get; set; }

        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase NoticePath { get; set; }

        public string noticeImgPath { get; set; }

        public string noticeUploadDate { get; set; }
    }
}