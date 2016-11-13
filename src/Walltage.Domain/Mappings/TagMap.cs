using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class TagMap : BaseEntityMap<Tag>
    {
        public TagMap()
        {
            Property(t => t.Name).IsRequired().HasMaxLength(100);

            ToTable("Tag");
        }
    }
}
