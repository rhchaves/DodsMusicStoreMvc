using DmStore.Areas.Admin.Models;
using DmStore.Areas.Admin.Models.Validations;
using DmStore.Areas.Admin.Repositories;
using DmStore.Areas.Admin.Services;
using DmStore.Data;
using DmStore.Models;
using DmStore.Models.Validations;
using DmStore.Repositories;
using DmStore.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllersWithViews();

// Configuração do DbContext do Oracle (para as outras entidades)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<DmStoreDbContext>(options =>
        options.UseOracle(connectionString));

builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuração do serviço Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DmStoreDbContext>()
                .AddDefaultTokenProviders();

// Registro de Validações
builder.Services.AddScoped<IValidator<Client>, ClientValidation>()
                .AddScoped<IValidator<Product>, ProductValidation>()
                .AddScoped<IValidator<Supplier>, SupplierValidation>();

// Registro dos Repositorios
builder.Services.AddScoped<IClientRepository, ClientRepository>()
                .AddScoped<IProductRepository, ProductRepository>()
                .AddScoped<ISupplierRepository, SupplierRepository>();

// Registro dos Serviços
builder.Services.AddScoped<IProductService, ProductService>()
                .AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IClienteServico, ClienteServico>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
