using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class GetTeacherDetails
    {
        public string teacherName { get; set; }

        public string teacherGender { get; set; }

        public string dob { get; set; }

        public string teacherDesignation { get; set; }

        public string subjectSpecilization { get; set; }

        public string teacherAddress { get; set; }

        public string teacherMobileNo { get; set; }

        public string teacherEmailID { get; set; }

        public string teacherPAN { get; set; }

        public string teacherAadhar { get; set; }

        public int noOfNotesUploaded { get; set; }

        public string imageAddress { get; set; }

        public string passwordStatus { get; set; }

        public string teacherStatus { get; set; }
    }
}