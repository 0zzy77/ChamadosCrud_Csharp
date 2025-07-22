using ChamadosCRUD.Data;
using ChamadosCRUD.Models.Seeds;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChamadosCRUDContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("ChamadosCRUDContext") ?? throw new InvalidOperationException("Connection string 'ChamadosCRUDContext' not found.")
        ));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Create";
        options.LogoutPath = "/Login/Logout";
        options.AccessDeniedPath = "/Login/AccessDenied";
    });

var app = builder.Build();

//Configuração das seeds
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedLocation.Initialize(services);
    SeedRole.Initialize(services);
    SeedStatusTicket.Initialize(services);
    SeedUserRoot.Initialize(services);
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
