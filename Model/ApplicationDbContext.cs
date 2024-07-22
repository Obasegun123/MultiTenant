using Microsoft.EntityFrameworkCore;
using MultiTenant.Service;

namespace MultiTenant.Model
{
    public class ApplicationDbContext:DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId { get; set; }
        public ApplicationDbContext(ICurrentTenantService currentTenantService, DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Tenant> Tenants { get; set; }

        // On Model Creating - multitenancy query filters 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Product>().HasQueryFilter(a => a.TenantId == CurrentTenantId);
        }

        // On Save Changes - write tenant Id to table

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }
    }
}
