﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Persistence;

namespace OnlineShop.Persistence.Migrations
{
    [DbContext(typeof(OnlineShopDbContext))]
    [Migration("20190517133928_AddProductIdInFilesTable")]
    partial class AddProductIdInFilesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("OnlineShop.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Street");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.FileDescription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FileName");

                    b.Property<string>("FileType");

                    b.Property<string>("OriginalName");

                    b.Property<long>("Size");

                    b.HasKey("Id");

                    b.ToTable("FileDescriptions");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("Length");

                    b.Property<string>("MimeType");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<int?>("ProductId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Number");

                    b.Property<DateTime>("OrderDate");

                    b.Property<int?>("StatusId");

                    b.Property<double>("TotalAmount");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.OrderLine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OrderId");

                    b.Property<double>("Price");

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderLines");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.OrderStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OrderStatus");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("SubCategory");

                    b.Property<double>("UnitPrice");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ProductCharacteristics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Key");

                    b.Property<int?>("ProductId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductCharacteristics");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Expires");

                    b.Property<string>("Token");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ShoppingCartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProductId");

                    b.Property<int>("Quantity");

                    b.Property<int>("ShoppingCartId");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShoppingCartId");

                    b.ToTable("ShoppingCartItems");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Phone");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Address", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Image", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.Order", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.OrderStatus", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("OnlineShop.Domain.Entities.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.OrderLine", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.Order", "Order")
                        .WithMany("OrderLines")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Domain.Entities.Product", "Product")
                        .WithMany("OrderLines")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ProductCharacteristics", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.Product", "Product")
                        .WithMany("Characteristics")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.User", "User")
                        .WithOne("RefreshToken")
                        .HasForeignKey("OnlineShop.Domain.Entities.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ShoppingCart", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.User", "User")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("OnlineShop.Domain.Entities.ShoppingCart", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OnlineShop.Domain.Entities.ShoppingCartItem", b =>
                {
                    b.HasOne("OnlineShop.Domain.Entities.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("OnlineShop.Domain.Entities.ShoppingCart", "ShoppingCart")
                        .WithMany("CartItems")
                        .HasForeignKey("ShoppingCartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
