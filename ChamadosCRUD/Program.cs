using ChamadosCRUD.Data;
using Microsoft.EntityFrameworkCore;
using ChamadosCRUD.Models.Seeds;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ChamadosCRUDContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("ChamadosCRUDContext") ?? throw new InvalidOperationException("Connection string 'ChamadosCRUDContext' not found.")
        ));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//Configuração das seeds
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedLocation.Initialize(services);
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
