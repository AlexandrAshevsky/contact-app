using Microsoft.EntityFrameworkCore;

using ContactEntity = Contact.Entity.Contact;

namespace Contact.Database
{
    public class ContactDbContext : DbContext
    {
        public DbSet<ContactEntity> Contacts { get; set; }

        public ContactDbContext(DbContextOptions options) : base(options) 
        { 
        }
    }
}
