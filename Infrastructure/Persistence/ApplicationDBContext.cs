using Application.Common.Interface;
using Domain.Common;
using Domain.Master;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDBContext : DbContext, IApplicationDBContext
    {
        #region Properties
        private readonly DateTime _currentDateTime;
        private readonly ICurrentUserService _currentUserService;
        #endregion
        #region Ctor
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, ICurrentUserService currentUserService)
          : base(options)
        {
            _currentDateTime = DateTime.Now;
            _currentUserService = currentUserService;
        }
        #endregion

        #region Master
        public DbSet<AppSetting> AppSettings { get; set; }
        public DbSet<ReferenceField> ReferenceFields { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        #endregion

        public async Task<int> SaveChangesAsync()
        {

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                var currentUserEmail = "jannatul14.baki@gmail.com"; //(await _currentUserService.GetCurrentUserAsync()).UserEmail;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Author = currentUserEmail; //Get Current UsereID
                        entry.Entity.Created = _currentDateTime;
                        entry.Entity.Editor = currentUserEmail; //Get Current UsereID
                        entry.Entity.Modified = _currentDateTime;
                        break;
                    case EntityState.Modified:
                        entry.Entity.Editor = currentUserEmail; //Get Current UsereID
                        entry.Entity.Modified = _currentDateTime;
                        break;
                }
            }
            return await base.SaveChangesAsync();
        }

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <returns>A set for the given entity type</returns>
        public virtual new DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                         .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType)))
            {
                var builder = modelBuilder.Entity(entityType.ClrType);

                builder.Property(nameof(IAuditableEntity.Created))
                    .HasColumnType("datetime")
                    .IsRequired();

                builder.Property(nameof(IAuditableEntity.Author))
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(nameof(IAuditableEntity.Modified))
                    .HasColumnType("datetime")
                    .IsRequired();

                builder.Property(nameof(IAuditableEntity.Editor))
                    .HasMaxLength(100)
                    .IsRequired();
            }
        }
    }
}
