﻿using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Divisions> Divisions { get; set; }
        public DbSet<Absence> Absences { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Employee>()
                        .HasOne<User>(e => e.User)
                        .WithOne(u => u.Employee)
                        .HasForeignKey<Employee>(u => u.EmployeeId);

            //modelBuilder.Entity<Absence>()
            //            .HasOne<User>(e => e.User)
            //            .WithOne(u => u.Absence)
            //            .HasForeignKey<Absence>(u => u.UserId);


        }
        //protected override void onmodelcreating(modelbuilder modelbuilder)
        //{
        //    modelbuilder.entity<userrole>().haskey(ur => new { ur.userid, ur.roleid });

        //    modelbuilder.entity<userrole>()
        //        .hasone(ur => ur.user)
        //        .withmany(u => u.userroles)
        //        .hasforeignkey(ur => ur.userid);

        //    modelbuilder.entity<userrole>()
        //        .hasone(ur => ur.role)
        //        .withmany(r => r.userroles)
        //        .hasforeignkey(ur => ur.roleid);
        //}

    }
}
