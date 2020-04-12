using Cityton.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cityton.Api.Data.Mapping
{
    public class CompanyMap
    {

        public CompanyMap(EntityTypeBuilder<Company> entityBuilder)
        {
            entityBuilder.HasKey(c => c.Id);
            entityBuilder.Property(c => c.Name).IsRequired();
            entityBuilder.HasIndex(c => c.Name).IsUnique();

            entityBuilder.Property(c => c.MinGroupSize).IsRequired();
            entityBuilder.Property(c => c.MaxGroupSize).IsRequired();
            entityBuilder.Property(c => c.CreatedAt).IsRequired();

            /*****/

            entityBuilder.HasMany(c => c.Users).WithOne(u => u.Company).HasForeignKey(u => u.CompanyId);

            /*****/

            //entityBuilder.Ignore(c => c.Users);
        }

    }
}