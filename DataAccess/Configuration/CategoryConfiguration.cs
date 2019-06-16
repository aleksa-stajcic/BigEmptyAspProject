using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class CategoryConfiguration : IEntityTypeConfiguration<Category> {
        public void Configure(EntityTypeBuilder<Category> builder) {

            builder.HasIndex(q => q.Name).IsUnique();
            builder.Property(q => q.Name).IsRequired();

            builder.Property(q => q.Description).IsRequired();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
