using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using lb6_server.Models.Entity;

namespace lb6_server.Repositories.EntityConfigurations
{
    public class UserEntityTypeConfiguration
    : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            _ = builder.Property(ci => ci.Id)
            .IsRequired();
            _ = builder.HasKey(ci => ci.Id);

            _ = builder.Property(cb => cb.Name)
                .IsRequired()
                .HasMaxLength(100);
            _ = builder.Property(cb => cb.Password)
                .IsRequired()
                .HasMaxLength(100);
            _ = builder.Property(cb => cb.Account)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
