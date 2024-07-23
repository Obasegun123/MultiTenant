using MultiTenant.Service;

namespace MultiTenant.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }

        // Get Tenant Id from incoming requests 
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            var tenantFromQuery = context.Request.Query["tenant"]; // Tenant Id from query string
            if (!string.IsNullOrEmpty(tenantFromQuery))
            {
                await currentTenantService.SetTenant(tenantFromQuery);
            }

            await _next(context);
        }


    }
}
