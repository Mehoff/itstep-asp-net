﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using itstep_shop.Models;

namespace itstep_shop.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20210712180205_UserCart_1")]
    partial class UserCart_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("itstep_shop.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("itstep_shop.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Фрукты"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Овощи"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Безалкогольные напитки"
                        });
                });

            modelBuilder.Entity("itstep_shop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            ImageUri = "https://clipart-best.com/img/banana/banana-clip-art-18.png",
                            Name = "Банан"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            ImageUri = "https://purepng.com/public/uploads/large/purepng.com-cucumbercucumbervegetablespicklegreenfoodhealthycucumbers-481522161925n6wbx.png",
                            Name = "Огурец"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            ImageUri = "https://pngimg.com/uploads/cocacola/cocacola_PNG21.png",
                            Name = "Coca-Cola"
                        });
                });

            modelBuilder.Entity("itstep_shop.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "user"
                        });
                });

            modelBuilder.Entity("itstep_shop.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CartId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CartId")
                        .IsUnique()
                        .HasFilter("[CartId] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "admin@gmail.com",
                            Name = "Администратор",
                            Password = "qwerty",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            Email = "max@gmail.com",
                            Name = "Макс",
                            Password = "qwerty",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("itstep_shop.Models.Cart", b =>
                {
                    b.HasOne("itstep_shop.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("itstep_shop.Models.Product", b =>
                {
                    b.HasOne("itstep_shop.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("itstep_shop.Models.User", b =>
                {
                    b.HasOne("itstep_shop.Models.Cart", "Cart")
                        .WithOne("User")
                        .HasForeignKey("itstep_shop.Models.User", "CartId");

                    b.HasOne("itstep_shop.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Cart");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("itstep_shop.Models.Cart", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("itstep_shop.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("itstep_shop.Models.Role", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
