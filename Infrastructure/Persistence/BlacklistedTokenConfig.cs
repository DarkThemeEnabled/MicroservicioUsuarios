using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence
{
    public class BlacklistedTokenConfig : IEntityTypeConfiguration<BlacklistedToken>
    {
        public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
        {
            builder.ToTable("BlacklistedTokens");

            builder.HasKey(b => b.Id);
            builder.Property(b => b.Token).IsRequired();
            builder.Property(b => b.AddedDate).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(b => b.ExpiryDate).IsRequired();
        }
    }
}