﻿// <auto-generated />
using System;
using Books2Gather.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Books2Gather.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250228135607_Buecherei V3")]
    partial class BuechereiV3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Books2Gather.Models.Author", b =>
                {
                    b.Property<int>("AuthorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("AuthorId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AuthorId"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("LastName");

                    b.HasKey("AuthorId");

                    b.ToTable("Authors");

                    b.HasData(
                        new
                        {
                            AuthorId = 1,
                            FirstName = "George R. R.",
                            LastName = "Martin"
                        },
                        new
                        {
                            AuthorId = 2,
                            FirstName = "William",
                            LastName = "Shakespeare"
                        });
                });

            modelBuilder.Entity("Books2Gather.Models.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("BookId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("AuthorId");

                    b.Property<int>("GenreId")
                        .HasColumnType("int")
                        .HasColumnName("GenreId");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("ISBN");

                    b.Property<decimal>("Prize")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Prize");

                    b.Property<DateOnly>("PublishingDate")
                        .HasColumnType("date")
                        .HasColumnName("PublishingDate");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("Title");

                    b.HasKey("BookId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("GenreId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            AuthorId = 1,
                            GenreId = 1,
                            ISBN = "978-3-608-93000-1",
                            Prize = 29.99m,
                            PublishingDate = new DateOnly(1954, 7, 29),
                            Title = "Der Herr der Ringe"
                        },
                        new
                        {
                            BookId = 2,
                            AuthorId = 2,
                            GenreId = 2,
                            ISBN = "978-3-551-55118-4",
                            Prize = 24.99m,
                            PublishingDate = new DateOnly(1997, 6, 26),
                            Title = "Harry Potter und der Stein der Weisen"
                        },
                        new
                        {
                            BookId = 3,
                            AuthorId = 1,
                            GenreId = 1,
                            ISBN = "978-3-608-93000-1",
                            Prize = 19.99m,
                            PublishingDate = new DateOnly(1937, 9, 21),
                            Title = "Der Hobbit"
                        });
                });

            modelBuilder.Entity("Books2Gather.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("GenreId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Description");

                    b.HasKey("GenreId");

                    b.ToTable("Genres");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            Description = "Fantasy"
                        },
                        new
                        {
                            GenreId = 2,
                            Description = "Jugendbuch"
                        });
                });

            modelBuilder.Entity("Books2Gather.Models.Book", b =>
                {
                    b.HasOne("Books2Gather.Models.Author", null)
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Books2Gather.Models.Genre", null)
                        .WithMany("Books")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Books2Gather.Models.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("Books2Gather.Models.Genre", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
