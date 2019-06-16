using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class ProductRatingConfiguration : IEntityTypeConfiguration<ProductRating> {
        public void Configure(EntityTypeBuilder<ProductRating> builder) {

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
