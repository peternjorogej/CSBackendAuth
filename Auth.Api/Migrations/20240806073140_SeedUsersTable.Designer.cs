﻿// <auto-generated />
using System;
using Auth.Api.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Auth.Api.Migrations
{
    [DbContext(typeof(SqliteDatabaseService))]
    [Migration("20240806073140_SeedUsersTable")]
    partial class SeedUsersTable
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

            modelBuilder.Entity("Auth.Api.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = "046f7ea9-9184-4367-9250-c9a3aae61f73",
                            AccessFailedCount = 0,
                            ClientId = 1,
                            ConcurrencyStamp = "424705e3-a199-42ad-a95b-ef96890ae98e",
                            Email = "michaelmathu@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEIlWoPiqTxSQ7wq5UP5Izm+12gfXBjFhke5Fw6oi/dJhaegXkMhKbsDAyzvNb0kpUA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a8556a46-d3da-4eff-9ec6-ddc57c838bcb",
                            TwoFactorEnabled = false,
                            UserName = "michaelmathu"
                        },
                        new
                        {
                            Id = "52feb4a1-353c-40f0-9e35-3da9e59c03d2",
                            AccessFailedCount = 0,
                            ClientId = 2,
                            ConcurrencyStamp = "d57e21d9-faa6-4cbf-ab24-19d90c4342ab",
                            Email = "peternjoroge@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            PasswordHash = "AQAAAAIAAYagAAAAEOYx/mzLWatBkV1aMFdAaayUBJCDexLF3t0mdVFB/e5jNiR+GyQMpZuRmwk+jbxW6A==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2d6688c7-3463-4498-b307-5d483e1d2018",
                            TwoFactorEnabled = false,
                            UserName = "peternjoroge"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
