using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class UserRoleMap : BaseEntityMap<UserRole>
    {
        public UserRoleMap()
        {
            Property(t => t.Name).IsRequired().HasMaxLength(20);

            ToTable("UserRole");
        }
    }
}
