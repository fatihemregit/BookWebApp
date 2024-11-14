﻿// <auto-generated />
using BookWebApp.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241114132546_firstmig")]
    partial class firstmig
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookWebApp.Models.Dto.BookDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Sefiller",
                            Price = 100m,
                            Writer = "Victor Hugo"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Suç ve Ceza",
                            Price = 150m,
                            Writer = "Fyodor Dostoevsky"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Karamazov Kardeşler",
                            Price = 150m,
                            Writer = "Fyodor Dostoevsky"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Tutunamayanlar",
                            Price = 150m,
                            Writer = "Oğuz Atay"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Sinekli Bakkal",
                            Price = 150m,
                            Writer = "Halide Edip Adıvar"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
