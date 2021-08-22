using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateTeacherModel
    {

        [Display(Name = "Teacher ID")]
        [Required(ErrorMessage = "*")]
        public int teacherID { get; set; }



        [Display(Name = "Teacher Name")]
        [Required(ErrorMessage = "*")]
        [StringLength(60, ErrorMessage = "Max 60 chars")]
        public string teacherName { get; set; }



        [Display(Name = "Teacher Gender")]
        [Required(ErrorMessage = "*")]
        [StringLength(10, ErrorMessage = "Max 10 chars")]
        public string teacherGender { get; set; }




        [Display(Name = "Teacher DoB")]
        [Required(ErrorMessage = "*")]
        //public DateTime dob { get; set; }
        public string dob { get; set; }



        [Display(Name = "Teacher Designation")]
        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "Max 40 chars")]
        public string teacherDesignation { get; set; }


        [Display(Name = "Teacher Qualification")]
        [Required(ErrorMessage = "*")]
        [StringLength(40, ErrorMessage = "Max 40 chars")]
        public string TeacherQualification { get; set; }

        [Display(Name = "Teacher Specilization")]
        //[Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Max 30 chars")]
        public string subjectSpecilization { get; set; }



        [Display(Name = "Teacher Address")]
        [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 chars")]
        public string teacherAddress { get; set; }




        [Display(Name = "Teacher Mobile No")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 digits")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "invalid format")]
        [Required(ErrorMessage = "*")]
        public string teacherMobileNo { get; set; }



        [Display(Name = "Teacher Email ID")]
        //[Required(ErrorMessage = "*")]
        [EmailAddress(ErrorMessage = "Invalid format")]
        [StringLength(50, ErrorMessage = "Max 50 chars")]
        public string teacherEmailID { get; set; }



        [Display(Name = "Teacher PAN No")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Only 10 Characters")]
        // [Required(ErrorMessage = "*")]
        public string teacherPAN { get; set; }



        [Display(Name = "Teacher Aadhar No")]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Only 12 digits")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "invalid format")]
        //[Required(ErrorMessage = "*")]
        public string teacherAadhar { get; set; }




        [Display(Name = "No of notes uploaded")]
        [Required(ErrorMessage = "*")]
        public int noOfNotesUploaded { get; set; }



        [Display(Name = "Upload passport size photo")]
        // [Required(ErrorMessage = "*")]
        [StringLength(100, ErrorMessage = "Max 100 chars")]
        public string imageAddress { get; set; }



        [Display(Name = "Select Teacher Status")]
        //[Required(ErrorMessage = "*")] 
        public string teacherStatus { get; set; }


       
    }
}