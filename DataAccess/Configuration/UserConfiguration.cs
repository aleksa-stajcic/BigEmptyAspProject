using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class UserConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {

            builder.HasIndex(q => q.Username).IsUnique();
            builder.Property(q => q.Username).IsRequired();

            builder.Property(q => q.FirstName).IsRequired();
            builder.Property(q => q.LastName).IsRequired();
            builder.Property(q => q.Email).IsRequired();
            


            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
