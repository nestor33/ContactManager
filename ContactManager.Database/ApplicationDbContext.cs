using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManager.Database
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base (options)
        {
            
        }

        public DbSet<ContactEntity> Contacts { get; set; }

       
    }
}
