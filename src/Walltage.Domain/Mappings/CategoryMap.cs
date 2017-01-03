using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class CategoryMap : BaseEntityMap<Category>
    {
        public CategoryMap()
        {
            Property(t => t.Name).IsRequired().HasMaxLength(50);

            ToTable("Category");
        }
    }
}
