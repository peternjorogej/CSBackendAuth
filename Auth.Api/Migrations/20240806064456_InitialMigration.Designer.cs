﻿// <auto-generated />
using Auth.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Auth.Api.Migrations
{
    [DbContext(typeof(SqliteDatabaseService))]
    [Migration("20240806064456_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.3");

            modelBuilder.Entity("Auth.Api.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Registration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "michaelmathu@gmail.com",
                            FirstName = "Michael",
                            LastName = "Mathu",
                            Registration = "BMIT-01/2023"
                        },
                        new
                        {
                            Id = 2,
                            Email = "peternjoroge@gmail.com",
                            FirstName = "Peter",
                            LastName = "Njoroge",
                            Registration = "BMIT-02/2023"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
