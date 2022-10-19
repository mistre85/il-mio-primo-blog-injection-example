using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using NetCore_01.Data;
using NetCore_01.Models.Repositories;
using NetCore_01.Models.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// iniettiamo la dipendenza --> tutti i costruttori che hanno all'interno IPostRepository, saranno inizializzati con DbPostRepository
builder.Services.AddScoped<IPostRepository, InMemoryPostRepository>();

builder.Services.AddScoped<BlogContext>();

var connectionString = builder.Configuration.GetConnectionString("BlogContextConnection") ?? throw new InvalidOperationException("Connection string 'BlogContextConnection' not found.");

builder.Services.AddDbContext<BlogContext>(options =>
    options.UseSqlServer(connectionString));;

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlogContext>();;

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
