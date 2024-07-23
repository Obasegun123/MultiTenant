using MultiTenant.Model;
using MultiTenant.Service.TenantService.DTO;

namespace MultiTenant.Service.TenantService
{
    public interface ITenantService
    {
        Tenant CreateTenant(CreateTenantRequest request);
    }
}
