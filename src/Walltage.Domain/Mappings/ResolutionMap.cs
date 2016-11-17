using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class ResolutionMap : BaseEntityMap<Resolution>
    {
        public ResolutionMap()
        {
            Property(t => t.Name).IsRequired().HasMaxLength(50);

            ToTable("Resolution");
        }
    }
}
