﻿// <auto-generated />
using System;
using Assignment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Assignment.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230907201117_Abc")]
    partial class Abc
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Assignment.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<int>("AbsentCount")
                        .HasColumnType("int");

                    b.Property<string>("EmployeeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("EmployeeSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("OffdayCount")
                        .HasColumnType("int");

                    b.Property<int>("PresentCount")
                        .HasColumnType("int");

                    b.Property<int?>("SupervisorId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Assignment.Models.EmployeeAttendance", b =>
                {
                    b.Property<int>("EmployeeAttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeAttendanceId"));

                    b.Property<DateTime>("AttendanceDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAbsent")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOffday")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPresent")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeAttendanceId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeAttendances");
                });

            modelBuilder.Entity("Assignment.Models.MonthlyAttendanceReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmployeeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MonthName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PayableSalary")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalAbsent")
                        .HasColumnType("int");

                    b.Property<int>("TotalOffday")
                        .HasColumnType("int");

                    b.Property<int>("TotalPresent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MonthlyAttendanceReports");
                });

            modelBuilder.Entity("Assignment.Models.EmployeeAttendance", b =>
                {
                    b.HasOne("Assignment.Models.Employee", "Employee")
                        .WithMany("EmployeeAttendances")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Assignment.Models.Employee", b =>
                {
                    b.Navigation("EmployeeAttendances");
                });
#pragma warning restore 612, 618
        }
    }
}
