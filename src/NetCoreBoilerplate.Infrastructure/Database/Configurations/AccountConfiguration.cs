using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Infrastructure.Database.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            ConfigureProperties(builder);
            ConfigureRelationships(builder);
            ConfigureIndexes(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<Account> builder)
        {
            builder.Property(t => t.FirstName)
                .HasMaxLength(32);

            builder.Property(t => t.LastName)
                .HasMaxLength(64);

            builder.Property(t => t.Email)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(t => t.Password)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(t => t.VerificationToken)
                .HasMaxLength(128);

            builder.Property(t => t.ResetToken)
                .HasMaxLength(200);
        }

        private void ConfigureRelationships(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasMany(i => i.RefreshTokens)
                .WithOne(i => i.Account)
                .HasForeignKey(i => i.AccountId);
        }

        private void ConfigureIndexes(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasIndex(p => new { p.Email })
                .IsUnique();
        }
    }
}
