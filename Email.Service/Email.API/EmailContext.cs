using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.API
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions<EmailContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration<EmailModel>(new EmailConfiguration());
        }
    }
}
