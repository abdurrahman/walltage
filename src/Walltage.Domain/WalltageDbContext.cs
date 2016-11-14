using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using Walltage.Domain.Entities;
using Walltage.Domain.Mappings;

namespace Walltage.Domain
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class WalltageDbContext : DbContext
    {
        public WalltageDbContext()
        {
            Configure();
        }

        public WalltageDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configure();
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserAndFavoriteMapping> UserAndFavoriteMapping { get; set; }
        public virtual DbSet<UserAndLikeMapping> UserAndLikeMappings { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Wallpaper> Wallpapers { get; set; }
        public virtual DbSet<WallpaperAndTagMapping> WallpaperAndTagMappings { get; set; }

        private void Configure()
        {
            Database.SetInitializer<WalltageDbContext>(null);
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
            Configuration.UseDatabaseNullSemantics = true;
            Configuration.ValidateOnSaveEnabled = true;
            //DbConfiguration.SetConfiguration(new MySql.Data.Entity.MySqlEFConfiguration());
        }

        // You may get below error when you didnt use correct connection string in config file..
        // "The context cannot be used while the model is being created. This exception may be thrown if the context is used inside the OnModelCreating method or if the same context instance is accessed by multiple threads concurrently. Note that instance members of DbContext and related classes are not guaranteed to be thread safe."
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new WallpaperMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    public class WalltageDbContextMigrationConfiguration : DbMigrationsConfiguration<WalltageDbContext>
    {
        public WalltageDbContextMigrationConfiguration()
        {
            AutomaticMigrationDataLossAllowed = false;
            AutomaticMigrationsEnabled = false;

            // register mysql code generator
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }
    }
}
