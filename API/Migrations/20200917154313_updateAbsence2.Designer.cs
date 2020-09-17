﻿// <auto-generated />
using System;
using API.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20200917154313_updateAbsence2")]
    partial class updateAbsence2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.14-servicing-32113")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("API.Models.Absence", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTimeOffset>("TimeIn");

                    b.Property<DateTimeOffset>("TimeOut");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("Absence");
                });

            modelBuilder.Entity("API.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("createdDate");

                    b.Property<DateTimeOffset>("deletedDate");

                    b.Property<bool>("isDelete");

                    b.Property<DateTimeOffset>("updatedDate");

                    b.HasKey("Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("API.Models.Divisions", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Name");

                    b.Property<DateTimeOffset>("createdDate");

                    b.Property<DateTimeOffset>("deletedDate");

                    b.Property<bool>("isDelete");

                    b.Property<DateTimeOffset>("updatedDate");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Divisions");
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.Property<string>("EmployeeId");

                    b.Property<string>("Address");

                    b.Property<DateTimeOffset>("CreateDate");

                    b.Property<DateTimeOffset>("DeleteData");

                    b.Property<string>("Name");

                    b.Property<string>("Phone");

                    b.Property<DateTimeOffset>("UpdateDate");

                    b.Property<bool>("isDelete");

                    b.HasKey("EmployeeId");

                    b.ToTable("Tb_Employee");
                });

            modelBuilder.Entity("API.Models.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedName");

                    b.HasKey("Id");

                    b.ToTable("tb_m_role");
                });

            modelBuilder.Entity("API.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("tb_m_user");
                });

            modelBuilder.Entity("API.Models.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("tb_m_userrole");
                });

            modelBuilder.Entity("API.Models.Absence", b =>
                {
                    b.HasOne("API.Models.User", "User")
                        .WithOne("Absence")
                        .HasForeignKey("API.Models.Absence", "Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Divisions", b =>
                {
                    b.HasOne("API.Models.Department", "Departments")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.Employee", b =>
                {
                    b.HasOne("API.Models.User", "User")
                        .WithOne("Employee")
                        .HasForeignKey("API.Models.Employee", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("API.Models.UserRole", b =>
                {
                    b.HasOne("API.Models.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("API.Models.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
