﻿namespace MultiTenant.Service
{
    public interface ICurrentTenantService
    {
        string? ConnectionString { get; set; }
        string? TenantId { get; set; }
        public Task<bool> SetTenant(string tenant);
    }
}
