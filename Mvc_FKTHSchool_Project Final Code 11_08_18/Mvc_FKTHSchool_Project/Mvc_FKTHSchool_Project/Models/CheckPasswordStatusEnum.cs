using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc_FKTHSchool_Project.Models
{
    public enum CheckPasswordStatusEnum
    {
        Updated, NewUser, NotInRole, Deactivated, WrongPassword
    }
}