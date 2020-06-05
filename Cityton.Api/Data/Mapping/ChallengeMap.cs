using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class ChallengeMap
    {

        public ChallengeMap(EntityTypeBuilder<Challenge> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Statement).IsRequired();
            entityBuilder.HasIndex(c => c.Statement).IsUnique();
            entityBuilder.Property(c => c.Title).IsRequired();
            entityBuilder.Property(c => c.CreatedAt).IsRequired();

            /*****/

            entityBuilder.HasMany(c => c.ChallengeGivens).WithOne(cg => cg.Challenge).HasForeignKey(cg => cg.ChallengeId);
            entityBuilder.HasMany(c => c.Achievements).WithOne(a => a.FromChallenge).HasForeignKey(a => a.FromChallengeId);
        }

    }
}