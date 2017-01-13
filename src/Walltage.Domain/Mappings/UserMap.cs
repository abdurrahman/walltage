using Walltage.Domain.Entities;

namespace Walltage.Domain.Mappings
{
    public class UserMap : BaseEntityMap<User>
    {
        public UserMap()
        {
            Property(t => t.Email).IsRequired().HasMaxLength(245);
            Property(t => t.Username).IsRequired().HasMaxLength(50);
            Property(t => t.Password).IsRequired().HasMaxLength(50);
            Property(t => t.LastActivity).IsOptional();
            Property(t => t.IPAddress).IsOptional().HasMaxLength(45);
            
            HasRequired(t => t.UserRole).WithMany().HasForeignKey(t => t.UserRoleId).WillCascadeOnDelete(false);

            ToTable("User");
        }
    }
}