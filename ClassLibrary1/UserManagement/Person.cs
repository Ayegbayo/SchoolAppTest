using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolApp.Data.UserManagement
{
    public abstract class Person : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string NationalId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }        
    }
}
