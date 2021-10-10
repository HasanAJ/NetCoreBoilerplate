using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetCoreBoilerplate.Domain.Entities;

namespace NetCoreBoilerplate.Infrastructure.Database.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            ConfigureProperties(builder);
            ConfigureRelationships(builder);
            ConfigureIndexes(builder);
        }

        private void ConfigureProperties(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.Property(t => t.Token)
                .HasMaxLength(64);
        }

        private void ConfigureRelationships(EntityTypeBuilder<RefreshToken> builder)
        {

        }

        private void ConfigureIndexes(EntityTypeBuilder<RefreshToken> builder)
        {

        }

    }
}
