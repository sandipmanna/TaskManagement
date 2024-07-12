using DataAccess.Components.Interface;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataContext
{
    public class EFDataContext : DbContext
    {
        public DbSet<AssignedTask> Tasks { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dbo");

            modelBuilder.Entity<AssignedTask>()
                .HasKey(c => c.Id)
                .ToTable("Task", "dbo");

            modelBuilder.Entity<Activity>()
                .HasKey(c => c.Id)
                .ToTable("Activity", "dbo");

            modelBuilder.Entity<Tag>()
                .HasKey(c => c.Id)
                .ToTable("Tag", "dbo");
        }
    }
}
