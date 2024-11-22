using Microsoft.EntityFrameworkCore;
using RIESGOS_Y_RESPUESTAS.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPasswordHasher<Usuario>, PasswordHasher<Usuario>>();


// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion a base de datos-------------------------------------------------------------------

builder.Services.AddDbContext<DbgestorContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("conexion"))
);

//-------------------------------------------------------------------------------------------

// Configurar los servicios de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        // Configuración del middleware de cookies
        options.LoginPath = "/AccesoController1/Index"; // La ruta para iniciar sesión
        options.LogoutPath = "/AccesoController1/Salir"; // La ruta para cerrar sesión
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // El tiempo de expiración de la cookie
        options.SlidingExpiration = true; // Si quieres que la cookie se renueve con cada solicitud
    });
////-------------------------------------------------------------------------------------------

var app = builder.Build();

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
    pattern: "{controller=AccesoController1}/{action=Index}/{id?}");

app.Run();
