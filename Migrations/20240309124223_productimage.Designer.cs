﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScanAndGoApi.Context;

#nullable disable

namespace ScanAndGoApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240309124223_productimage")]
    partial class productimage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ScanAndGoApi.Models.Item", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Productid")
                        .HasColumnType("bigint");

                    b.Property<long>("StoreId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Productid");

                    b.HasIndex("StoreId");

                    b.ToTable("ITEM");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("PRODUCT");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.ProductListAsc", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("Productid")
                        .HasColumnType("bigint");

                    b.Property<long>("ShoppingListId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Productid");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("PRODUCT_LIST_ASC");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.ShoppingList", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<long>("Userid")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("Userid");

                    b.ToTable("SHOPPING_LIST");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.Store", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("STORE");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("USER");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.Item", b =>
                {
                    b.HasOne("ScanAndGoApi.Models.Product", "Product")
                        .WithMany("Items")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScanAndGoApi.Models.Store", "Store")
                        .WithMany("Items")
                        .HasForeignKey("StoreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.ProductListAsc", b =>
                {
                    b.HasOne("ScanAndGoApi.Models.Product", "Product")
                        .WithMany("ProductListAsc")
                        .HasForeignKey("Productid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScanAndGoApi.Models.ShoppingList", "ShoppingList")
                        .WithMany("ProductsInList")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.ShoppingList", b =>
                {
                    b.HasOne("ScanAndGoApi.Models.User", "User")
                        .WithMany("ShoppingLists")
                        .HasForeignKey("Userid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.Product", b =>
                {
                    b.Navigation("Items");

                    b.Navigation("ProductListAsc");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.ShoppingList", b =>
                {
                    b.Navigation("ProductsInList");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.Store", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("ScanAndGoApi.Models.User", b =>
                {
                    b.Navigation("ShoppingLists");
                });
#pragma warning restore 612, 618
        }
    }
}
