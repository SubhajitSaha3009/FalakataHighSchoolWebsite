using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class NotesModel
    {

        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int TeacherId { get; set; }




        public int NotesID { get; set; }

        //[Display(Name = "Teacher Name")]
        //[Required(ErrorMessage = "*")]
        //public string TeacherName { get; set; }



        [Display(Name = "Upload File")]
        [Required(ErrorMessage = "*")]
        public HttpPostedFileBase file { get; set; }

        //[Required(ErrorMessage = "*")]
        //public string FileName
        //{
        //    get
        //    {
        //        if (file != null)
        //            return file.FileName;
        //        else
        //            return String.Empty;
        //    }
        //}



        [Display(Name = "Class")]
        [Required(ErrorMessage = "*")]
        public int OfClass { get; set; }


        [Display(Name = "Section")]
        [Required(ErrorMessage = "*")]
        public string Section { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 chars")]
        public string NoteSDesc { get; set; }

    }
}