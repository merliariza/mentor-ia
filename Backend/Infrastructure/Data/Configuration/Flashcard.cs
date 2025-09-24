using Domain.Entities;                        
using Microsoft.EntityFrameworkCore;          
using Microsoft.EntityFrameworkCore.Metadata.Builders; 
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class FlashcardConfiguration : IEntityTypeConfiguration<Flashcard>
    {
        public void Configure(EntityTypeBuilder<Flashcard> builder)
        {
            builder.ToTable("Flashcard");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Question)
            .IsRequired();

            builder.Property(f => f.Answer)
            .IsRequired();

            builder.HasOne(f => f.EvaluationSession)
            .WithMany(es => es.Flashcards)
            .HasForeignKey(f => f.EvaluationSessionId)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}