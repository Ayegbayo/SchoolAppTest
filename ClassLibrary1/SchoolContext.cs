using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Data.UserManagement;
using System;

namespace ClassLibrary1
{
    public class SchoolAppContext : IdentityDbContext<User>
    {
        public SchoolAppContext(DbContextOptions<SchoolAppContext> options)
        {

        }
    }
}
