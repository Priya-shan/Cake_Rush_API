using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Cake_Rush_API.Models;

namespace Cake_Rush_API.Data
{
    public class Cake_Rush_APIContext : DbContext
    {
        public Cake_Rush_APIContext(DbContextOptions<Cake_Rush_APIContext> options)
            : base(options)
        {
        }

        public DbSet<CategoryModel> CategoryModel { get; set; } = default!;

        public DbSet<ProductModel>? ProductModel { get; set; }


        public DbSet<SubCategoryMapModel>? SubCategoryMapModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.categoryId);

            //modelBuilder.Entity<ProductModel>()
            //    .HasMany(p => p.SubCategoryMap)
            //    .WithOne(s => s.Product)
            //    .HasForeignKey(s => s.productId);

            modelBuilder.Entity<SubCategoryMapModel>()
                .HasOne(s => s.Product)
                .WithOne()
                .HasForeignKey<SubCategoryMapModel>(s => s.productId);

            //modelBuilder.Entity<UserModel>()
            //    .HasMany(u => u.carts)
            //    .WithOne(c => c.User)
            //    .HasForeignKey(c => c.userId);
            modelBuilder.Entity<CartModel>()
                .HasOne(c => c.User)
                .WithOne()
                .HasForeignKey<CartModel>(c => c.userId);

            //modelBuilder.Entity<SubCategoryMapModel>()
            //    .HasMany(s => s.carts)
            //    .WithOne(c => c.SubCatMap)
            //    .HasForeignKey(c => c.mapId);
            modelBuilder.Entity<CartModel>()
                .HasOne(c => c.SubCatMap)
                .WithOne()
                .HasForeignKey<CartModel>(c=>c.mapId);

            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Cart)
                .WithOne()
                .HasForeignKey<OrderModel>(c => c.cartId);

        }

        public DbSet<UserModel>? UserModel { get; set; }

        public DbSet<CartModel>? CartModel { get; set; }

        public DbSet<OrderModel>? OrderModel { get; set; }


    }
}