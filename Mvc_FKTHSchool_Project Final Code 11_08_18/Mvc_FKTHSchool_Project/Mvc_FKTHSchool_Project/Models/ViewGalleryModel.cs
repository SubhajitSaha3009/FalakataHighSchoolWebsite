using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public class ViewGalleryModel
    {
        public int ImageId { get; set; }
        
        public string title { get; set; }

        
        public string imageDescription { get; set; }

        
        public string imagePath { get; set; }

        public int uploaderID { get; set; }

        public string uploadDate { get; set; }
    }
}