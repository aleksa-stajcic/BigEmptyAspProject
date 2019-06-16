using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer> {
        public void Configure(EntityTypeBuilder<Manufacturer> builder) {

            builder.HasIndex(q => q.Name).IsUnique();
            builder.Property(q => q.Name).IsRequired();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
