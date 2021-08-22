using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class FeedbackModel
    {
        [Display(Name = "Feedback ID")]
        [Required(ErrorMessage = "*")]
        public int feedbackID { get; set; }

        [Display(Name = "Sender ID")]
        //[Required(ErrorMessage = "*")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string SenderID { get; set; }



        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(20, ErrorMessage = "Max 20 characters")]
        public string name { get; set; }


        [Display(Name = "Feedback Message")]
        [Required(ErrorMessage = "Please enter feedback")]
        [StringLength(300, ErrorMessage = "Max 300 characters")]
        public string feedbackMessage { get; set; }


       
    }
}