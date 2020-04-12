using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class AchievementMap
    {
        public AchievementMap(EntityTypeBuilder<Achievement> entityBuilder)
        {
            entityBuilder.HasKey(a => a.Id);
            entityBuilder.Property(a => a.UnlockedAt).IsRequired();

            /*****/

            //entityBuilder.Ignore(a => a.Winner);
            //entityBuilder.Ignore(a => a.FromChallenge);
        }

    }
}