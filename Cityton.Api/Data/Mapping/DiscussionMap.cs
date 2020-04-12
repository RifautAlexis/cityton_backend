using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class DiscussionMap
    {

        public DiscussionMap(EntityTypeBuilder<Discussion> entityBuilder)
        {
            entityBuilder.HasKey(d => d.Id);
            entityBuilder.Property(d => d.CreatedAt).IsRequired();
            entityBuilder.Property(d => d.Name).IsRequired(false).HasMaxLength(10);
            entityBuilder.HasIndex(u => u.Name).IsUnique();

            /*****/

            entityBuilder.HasMany(d => d.UsersInDiscussion).WithOne(uid => uid.Discussion).HasForeignKey(uid => uid.DiscussionId);
            entityBuilder.HasMany(d => d.Messages).WithOne(m => m.Discussion).HasForeignKey(m => m.DiscussionId);

            /*****/

            //entityBuilder.Ignore(d => d.UsersInDiscussion);
            //entityBuilder.Ignore(d => d.Messages);
        }

    }
}