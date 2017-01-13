using Walltage.Domain.Entities;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Mappings
{
    public abstract class BaseEntityMap<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        protected BaseEntityMap()
        {
            HasKey(t => t.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(t => t.AddedDate).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
            //Property(t => t.ModifiedDate).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}