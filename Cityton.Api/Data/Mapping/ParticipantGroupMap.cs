using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class ParticipantGroupMap
    {

        public ParticipantGroupMap(EntityTypeBuilder<ParticipantGroup> entityBuilder)
        {
            entityBuilder.HasKey(pg => pg.Id);
            entityBuilder.Property(pg => pg.IsCreator).IsRequired();
            entityBuilder.Property(pg => pg.Status).IsRequired();
        }

    }
}
