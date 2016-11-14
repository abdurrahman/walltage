using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Walltage.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}
