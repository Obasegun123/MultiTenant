namespace MultiTenant.Model
{
    public class Product:IMustHaveTenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string TenantId { get; set; }
    }
}
