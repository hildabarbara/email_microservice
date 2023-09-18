using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Email.API
{
    public class EmailConfiguration : IEntityTypeConfiguration<EmailModel>
    {
        public void Configure(EntityTypeBuilder<EmailModel> builder)
        {
            builder.HasKey(x => x.Id);            

            builder
                .Property(x => x.Email)
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}
