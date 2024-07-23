using Microsoft.EntityFrameworkCore;
using MultiTenant.Extensions;
using MultiTenant.Middleware;
using MultiTenant.Model;
using MultiTenant.Service;
using MultiTenant.Service.ProductService;
using MultiTenant.Service.TenantService;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// adding a database service with configuration -- connection string read from appsettings.json
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<TenantDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAndMigrateTenantDatabases(builder.Configuration);

// adding our own service -- CRUD services should be registered with transient lifetimes

// Current tenant service with scoped lifetime (created per each request)
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();
builder.Services.AddTransient<ITenantService, TenantService>();

builder.Services.AddTransient<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
