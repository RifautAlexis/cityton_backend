using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class ChallengeGivenMap
    {
        public ChallengeGivenMap(EntityTypeBuilder<ChallengeGiven> entityBuilder)
        {
            entityBuilder.HasKey(cg => cg.Id);
            entityBuilder.Property(cg => cg.Status).IsRequired();
        }

    }
}