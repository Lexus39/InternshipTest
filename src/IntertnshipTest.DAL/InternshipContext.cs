using IntertnshipTest.DAL.Configurations;
using IntertnshipTest.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntertnshipTest.DAL
{
    public class InternshipContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserGroupEntity> Groups { get; set; }
        public DbSet<UserStateEntity> States { get; set; }

        public InternshipContext(DbContextOptions<InternshipContext> options) 
            : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserGroupEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserStateEntityTypeConfiguration());
        }
    }
}
