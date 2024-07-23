using Microsoft.EntityFrameworkCore;
using MultiTenant.Model;

namespace MultiTenant.Service
{
    public class CurrentTenantService : ICurrentTenantService
    {
        private readonly TenantDbContext _context;
        public string? TenantId { get; set; }
        public string? ConnectionString { get; set; }

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;

        }
        public async Task<bool> SetTenant(string tenant)
        {

            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync(); // check if tenant exists
            if (tenantInfo != null)
            {
                TenantId = tenant;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid");
            }

        }
    }
}
