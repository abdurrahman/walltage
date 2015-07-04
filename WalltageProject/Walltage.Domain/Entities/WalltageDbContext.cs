using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using MySql.Data.Entity;

namespace Walltage.Domain.Entities
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class WalltageDbContext : DbContext
    {
        public DbSet<Wallpaper> Wallpapers { get; set; }
    }
}
