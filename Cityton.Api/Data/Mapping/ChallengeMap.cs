using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq;

namespace Cityton.Api.Data.Mapping
{
    public class ChallengeMap
    {

        public ChallengeMap(EntityTypeBuilder<Challenge> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Statement).IsRequired();
            entityBuilder.HasIndex(c => c.Statement).IsUnique();
            entityBuilder.Property(c => c.Name).IsRequired();
            entityBuilder.Property(c => c.Status).IsRequired();
            entityBuilder.Property(c => c.CreatedAt).IsRequired();

            /*****/

            entityBuilder.HasMany(c => c.ChallengeGivens).WithOne(cg => cg.Challenge).HasForeignKey(cg => cg.ChallengeId);
            entityBuilder.HasMany(c => c.Achievements).WithOne(a => a.FromChallenge).HasForeignKey(a => a.FromChallengeId);

            /*****/

            //entityBuilder.Ignore(c => c.Author);
            //entityBuilder.Ignore(c => c.Achievements);
            //entityBuilder.Ignore(c => c.ChallengeGivens);
        }

    }
}