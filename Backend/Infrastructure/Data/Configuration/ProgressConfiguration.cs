using Domain.Entities;                        
using Microsoft.EntityFrameworkCore;          
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
    {
        public void Configure(EntityTypeBuilder<Progress> builder)
        {
            builder.ToTable("Progress");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Score)
                .IsRequired();

            builder.Property(p => p.Topic)
                .IsRequired();

            builder.Property(p => p.Feedback)
                .IsRequired();

            builder.HasOne(p => p.UserMember)
                .WithMany(u => u.Progresses)
                .HasForeignKey(p => p.UserMemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.EvaluationSessions)
                .WithOne(es => es.Progress)
                .HasForeignKey(es => es.ProgressId);
        }
    }
}
