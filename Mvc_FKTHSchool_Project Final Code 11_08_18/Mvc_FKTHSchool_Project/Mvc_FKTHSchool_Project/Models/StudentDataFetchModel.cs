using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class StudentDataFetchModel
    {

        public string StdId { get; set; }

        public string StdName { get; set; }

        public String OfClass { get; set; }

        public string DOB { get; set; }

        public int Admsnyr { get; set; }

        public string GuardName { get; set; }

        public string MothrsName { get; set; }

        public string MobNO { get; set; }

        public string RStatus { get; set; }

        public string SStatus { get; set; }

        public string gender { get; set; }

        public string Address { get; set; }

        public string StdPass { get; set; }
    }
}