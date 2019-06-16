using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Configuration {
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail> {
        public void Configure(EntityTypeBuilder<OrderDetail> builder) {

            builder.Property(q => q.CreatedAt).HasDefaultValueSql("GETDATE()");

        }
    }
}
