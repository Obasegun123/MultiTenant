namespace MultiTenant.Model
{
    public interface IMustHaveTenant
    {
        public string TenantId { get; set; }
    }
}
