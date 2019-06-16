using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class PasswordConfiguration : IEntityTypeConfiguration<Password> {
        public void Configure(EntityTypeBuilder<Password> builder) {

            builder.Property(q => q.Hidden).IsRequired();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
