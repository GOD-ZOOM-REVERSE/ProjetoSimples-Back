using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProcessoManyminds_Back.Business;
using ProcessoManyminds_Back.Business.Interfaces;
using ProcessoManyminds_Back.Context;
using ProcessoManyminds_Back.Datas.Repository;
using ProcessoManyminds_Back.Datas.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddDbContext<ManymindsContext>(options =>
{
    options.UseSqlServer(connectionString);
});

var config = services.Configure<IdentityOptions>(options =>
{
    // Determinando como a senha deve ser válido
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;

    // configuração para tentativas de login
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 3;
}).AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ManymindsContext>()
    .AddSignInManager<SignInManager<IdentityUser>>()
    .AddDefaultTokenProviders();

services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.Cookie.Name = "YourAppCookieName";
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
    options.SlidingExpiration = true;
});

services.AddScoped<IProdutosBusiness, ProdutosBusiness>();
services.AddScoped<IPedidosComprasBusiness, PedidosComprasBusiness>();

services.AddScoped<IProdutosRepository, ProdutosRepository>();
services.AddScoped<IPedidosComprasRepository, PedidosComprasRepository>();

services.AddAuthorization();

services.AddControllers();

services.AddCors(options =>
{
    options.AddPolicy("_myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:3000").AllowCredentials()
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors("_myAllowSpecificOrigins");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
