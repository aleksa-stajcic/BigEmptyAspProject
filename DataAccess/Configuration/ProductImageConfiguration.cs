using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage> {
        public void Configure(EntityTypeBuilder<ProductImage> builder) {

            builder.Property(q => q.FileName).IsRequired();
            builder.Property(q => q.Alt).IsRequired();

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
