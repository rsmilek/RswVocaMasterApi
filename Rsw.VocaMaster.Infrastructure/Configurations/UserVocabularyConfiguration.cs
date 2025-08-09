using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rsw.VocaMaster.Domain.Entities;

namespace Rsw.VocaMaster.Infrastructure.Configurations;

public class UserVocabularyConfiguration : IEntityTypeConfiguration<UserVocabulary>
{
    public void Configure(EntityTypeBuilder<UserVocabulary> builder)
    {
        builder
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.Email)
            .HasKey(x => x.Email);
    }
}
