using Domain.Entities;                        
using Microsoft.EntityFrameworkCore;          
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class EvaluationSessionConfiguration : IEntityTypeConfiguration<EvaluationSession>
    {
        public void Configure(EntityTypeBuilder<EvaluationSession> builder)
        {
            builder.ToTable("EvaluationSession");

            builder.HasKey(es => es.Id);

            builder.Property(es => es.Score)
            .IsRequired();

            builder.Property(es => es.Feedback)
            .IsRequired();

            builder.HasOne(es => es.Progress)
            .WithMany(p => p.EvaluationSessions)
            .HasForeignKey(es => es.ProgressId)
            .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(es => es.Flashcards)
            .WithOne(f => f.EvaluationSession)
            .HasForeignKey(f => f.EvaluationSessionId);
        }
    }
}