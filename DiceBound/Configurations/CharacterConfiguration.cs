using DiceBound.Entity_s.Characters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasOne(c => c.User)
               .WithMany(u => u.Characters)
               .HasForeignKey(c => c.UserId);

        builder.HasOne(c => c.Race)
               .WithMany(r => r.Characters)
               .HasForeignKey(c => c.RaceId);
    }
}