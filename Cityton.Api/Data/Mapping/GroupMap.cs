using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class GroupMap
    {

        public GroupMap(EntityTypeBuilder<Group> entityBuilder)
        {
            entityBuilder.HasKey(g => g.Id);
            entityBuilder.Property(g => g.Name).IsRequired();
            entityBuilder.HasIndex(g => g.Name).IsUnique();

            entityBuilder.Property(g => g.CreatedAt).IsRequired();

            /*****/

            entityBuilder.HasMany(g => g.Members).WithOne(pg => pg.BelongingGroup).HasForeignKey(pg => pg.BelongingGroupId);
            entityBuilder.HasMany(g => g.ChallengesGiven).WithOne(cg => cg.ChallengedGroup).HasForeignKey(cg => cg.ChallengedGroupId);
            entityBuilder.HasOne(g => g.Discussion).WithOne(d => d.Group).HasForeignKey<Discussion>(d => d.GroupId);

            /*****/

            //entityBuilder.Ignore(g => g.Members);
            //entityBuilder.Ignore(g => g.ChallengesGiven);
        }

    }
}