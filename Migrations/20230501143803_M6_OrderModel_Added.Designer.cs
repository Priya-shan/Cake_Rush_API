﻿// <auto-generated />
using System;
using Cake_Rush_API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cake_Rush_API.Migrations
{
    [DbContext(typeof(Cake_Rush_APIContext))]
    [Migration("20230501143803_M6_OrderModel_Added")]
    partial class M6_OrderModel_Added
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Cake_Rush_API.Models.CartModel", b =>
                {
                    b.Property<int>("cartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("cartId"), 1L, 1);

                    b.Property<int>("mapId")
                        .HasColumnType("int");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<string>("userId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("cartId");

                    b.HasIndex("mapId");

                    b.HasIndex("userId");

                    b.ToTable("CartModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.CategoryModel", b =>
                {
                    b.Property<int>("categoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("categoryId"), 1L, 1);

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("categoryId");

                    b.ToTable("CategoryModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.OrderModel", b =>
                {
                    b.Property<int>("orderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("orderId"), 1L, 1);

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<int>("cartId")
                        .HasColumnType("int");

                    b.Property<DateTime>("dateOrdered")
                        .HasColumnType("datetime2");

                    b.Property<string>("message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("orderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentMedium")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("paymentMode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("orderId");

                    b.HasIndex("cartId");

                    b.ToTable("OrderModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.ProductModel", b =>
                {
                    b.Property<int>("productId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("productId"), 1L, 1);

                    b.Property<int>("categoryId")
                        .HasColumnType("int");

                    b.Property<string>("imageId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("label")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<string>("productDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("productId");

                    b.HasIndex("categoryId");

                    b.ToTable("ProductModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.SubCategoryMapModel", b =>
                {
                    b.Property<int>("mapId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("mapId"), 1L, 1);

                    b.Property<string>("categoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("productId")
                        .HasColumnType("int");

                    b.HasKey("mapId");

                    b.HasIndex("productId");

                    b.ToTable("SubCategoryMapModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.UserModel", b =>
                {
                    b.Property<string>("userId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pincode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("userId");

                    b.ToTable("UserModel");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.CartModel", b =>
                {
                    b.HasOne("Cake_Rush_API.Models.SubCategoryMapModel", "SubCatMap")
                        .WithMany("carts")
                        .HasForeignKey("mapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cake_Rush_API.Models.UserModel", "User")
                        .WithMany("carts")
                        .HasForeignKey("userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SubCatMap");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.OrderModel", b =>
                {
                    b.HasOne("Cake_Rush_API.Models.CartModel", "Cart")
                        .WithMany("order")
                        .HasForeignKey("cartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.ProductModel", b =>
                {
                    b.HasOne("Cake_Rush_API.Models.CategoryModel", "Category")
                        .WithMany("Products")
                        .HasForeignKey("categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.SubCategoryMapModel", b =>
                {
                    b.HasOne("Cake_Rush_API.Models.ProductModel", "Product")
                        .WithMany("SubCategoryMap")
                        .HasForeignKey("productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.CartModel", b =>
                {
                    b.Navigation("order");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.CategoryModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.ProductModel", b =>
                {
                    b.Navigation("SubCategoryMap");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.SubCategoryMapModel", b =>
                {
                    b.Navigation("carts");
                });

            modelBuilder.Entity("Cake_Rush_API.Models.UserModel", b =>
                {
                    b.Navigation("carts");
                });
#pragma warning restore 612, 618
        }
    }
}
