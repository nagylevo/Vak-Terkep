using Microsoft.AspNetCore.Authentication;
using Vak_Terkep.Implementations;
using Vak_Terkep.Interfaces;
using Vak_Terkep.Models;
using Vak_Terkep.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VakTerkepDbContext>();

builder.Services.AddSession(options =>
{

	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IUserInterface, DatabaseUserManager>();
builder.Services.AddScoped<IAuthenticationServices, Vak_Terkep.Implementations.AuthenticationService>();
builder.Services.AddScoped<IEncryptService, EncryptService>();
builder.Services.AddScoped<IRouting, RoutingService>();


builder.Services.AddScoped<DataSeeder>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
	seeder.SeedData();  
}

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");

	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Map}/{action=Start}/{id?}");



app.Run();
