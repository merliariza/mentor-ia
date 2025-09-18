using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class UserMemberConfiguration : IEntityTypeConfiguration<UserMember>
    {
        public void Configure(EntityTypeBuilder<UserMember> builder)
        {
            builder.ToTable("UserMember");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.FullName)
            .IsRequired();

            builder.Property(u => u.Email)
            .IsRequired();

            builder.Property(u => u.PasswordHash)
            .IsRequired();

            builder.HasMany(u => u.Progresses)
                .WithOne(p => p.UserMember)
                .HasForeignKey(p => p.UserMemberId);
        }
    }
}