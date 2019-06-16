using DataAccess.Configuration;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess {
    public class BigEmptyContext : DbContext {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {

            optionsBuilder.UseSqlServer("Data Source=DESKTOP-KO5FVPV;Initial Catalog=BigEmpty;Integrated Security=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {


            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new PasswordConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
            modelBuilder.ApplyConfiguration(new ProductRatingConfiguration());

            modelBuilder.Entity<Role>().HasData(new Role {
                Id = Role.DefaultGroupId,
                Name = "Admin"
            });

            modelBuilder.Entity<User>().HasData(new User {
                Id = User.DefaultUserId,
                RoleId = Role.DefaultGroupId,
                FirstName = "Monty",
                LastName = "Python",
                Username = "superadmin",
                Email = "php1.store.test@gmail.com"
            });

            modelBuilder.Entity<Password>().HasData(new Password {
                Id = Password.DefaultHiddenId,
                UserId = User.DefaultUserId,
                Hidden = "Pwd1234!"
            });

        }
    }
}
