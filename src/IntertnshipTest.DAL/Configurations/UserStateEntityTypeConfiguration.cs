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
    public class UserStateEntityTypeConfiguration : IEntityTypeConfiguration<UserStateEntity>
    {
        public void Configure(EntityTypeBuilder<UserStateEntity> builder)
        {
            builder.ToTable("user_states");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id")
                .IsRequired();

            builder.Property(s => s.Code)
                .HasColumnName("code")
                .IsRequired();

            builder.Property(s => s.Description)
                .HasColumnName("description")
                .IsRequired();
        }
    }
}
