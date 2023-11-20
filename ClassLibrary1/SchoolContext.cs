using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.UserManagement;
using System;

namespace ClassLibrary1
{
    public class SchoolAppContext : IdentityDbContext<IdentityUser>
    {
        public SchoolAppContext(DbContextOptions<SchoolAppContext> options)
        {

        }
    }
}
