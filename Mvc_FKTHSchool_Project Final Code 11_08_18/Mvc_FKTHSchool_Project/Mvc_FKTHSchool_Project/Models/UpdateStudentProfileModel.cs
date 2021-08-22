using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Mvc_FKTHSchool_Project.Models
{
    public class UpdateStudentProfileModel
    {
        [Display(Name = "Student Id")]
        [StringLength(8, ErrorMessage = "Max 8 characters")]
        public string studentId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(60, ErrorMessage = "Max 60 characters")]
        public string name { get; set; }


        [Display(Name = "Gender")]
        [Required(ErrorMessage = "*")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(10, ErrorMessage = "Max 10 characters")]
        public string studentGender { get; set; }

        [Display(Name = "Father's/Guardian's Name")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(60, ErrorMessage = "Max 60 characters")]
        [Required(ErrorMessage = "*")]
        public string father_guardian_name { get; set; }


        [Display(Name = "Relation with guardian")]
        [RegularExpression("([A-Za-z ]*)", ErrorMessage = "invalid format")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        [Required(ErrorMessage = "*")]
        public string relationWithGuadrian { get; set; }

        [Display(Name = "Guardian's Mobile No.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Max 10 numbers")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "invalid format")]
        [Required(ErrorMessage = "*")]
        public String guardian_mob_no { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Max 100 characters")]
        [Required(ErrorMessage = "*")]
        public string address { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string class_name { get; set; }

        [Display(Name = "Result Status")]
        [Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string resultStatus { get; set; }

        [Display(Name = "Student Status")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        [Required(ErrorMessage = "*")]
        public string studentStatus { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string password { get; set; }
    
    }
}