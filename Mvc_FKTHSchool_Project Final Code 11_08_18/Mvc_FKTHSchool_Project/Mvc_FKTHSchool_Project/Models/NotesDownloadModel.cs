using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class NotesDownloadModel
    {
        
        //public string NotesPath { get; set; }

        public int Class { get; set; }

        public string NotesPath { get; set; }

        public string Section { get; set; }

        public string NotesDesc { get; set; }
    }
}