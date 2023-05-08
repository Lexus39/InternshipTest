using IntertnshipTest.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntertnshipTest.DAL.Configurations
{
    public class UserGroupEntityTypeConfiguration : IEntityTypeConfiguration<UserGroupEntity>
    {
        public void Configure(EntityTypeBuilder<UserGroupEntity> builder)
        {
            builder.ToTable("user_groups");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(g => g.Code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(g => g.Description)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}
