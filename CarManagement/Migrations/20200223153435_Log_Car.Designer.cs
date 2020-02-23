﻿// <auto-generated />
using System;
using CarManagement.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200223153435_Log_Car")]
    partial class Log_Car
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarManagement.Models.Car", b =>
                {
                    b.Property<string>("szCarId")
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("dtmCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtmUpdated")
                        .HasColumnType("datetime2");

                    b.Property<string>("szCarModel")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("szCarName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("szCarId");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("CarManagement.Models.Log", b =>
                {
                    b.Property<byte[]>("gdHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("binary(16)");

                    b.Property<DateTime>("dtmCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("szAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("szData")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("szEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("szUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("gdHistoryId");

                    b.ToTable("Log");
                });
#pragma warning restore 612, 618
        }
    }
}