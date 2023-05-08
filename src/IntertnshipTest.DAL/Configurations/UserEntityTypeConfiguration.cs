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
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.HasAlternateKey(u => u.Login);

            builder.Property(u => u.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(u => u.Login)
                .HasColumnName("login")
                .IsRequired();

            builder.Property(u => u.Password)
                .HasColumnName("password")
                .IsRequired();

            builder.Property(u => u.CreatedDate)
                .HasColumnName("created_date")
                .IsRequired();

            builder.Property(u => u.UserGroupId)
                .HasColumnName("user_group_id")
                .IsRequired();

            builder.Property(u => u.UserStateId)
                .HasColumnName("user_state_id")
                .IsRequired();

            builder.HasOne(u => u.UserGroup)
                .WithMany()
                .HasForeignKey(u => u.UserGroupId);

            builder.HasOne(u => u.UserState)
                .WithMany()
                .HasForeignKey(u => u.UserStateId);
        }
    }
}
