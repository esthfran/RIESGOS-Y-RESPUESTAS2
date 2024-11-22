using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using RIESGOS_Y_RESPUESTAS.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace RIESGOS_Y_RESPUESTAS.Controllers
{
    public class AccesoController1 : Controller
    {
        private readonly DbgestorContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AccesoController1(DbgestorContext context, IPasswordHasher<Usuario> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Usuario _usuario)
        {
            if (string.IsNullOrEmpty(_usuario.Correo) || string.IsNullOrEmpty(_usuario.Contraseña))
            {
                ViewBag.Error = "Correo y contraseña son obligatorios.";
                return View();
            }

            // Buscar al usuario en la base de datos
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == _usuario.Correo);

            if (usuario == null)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            // Verificar la contraseña hasheada
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(usuario, usuario.Contraseña, _usuario.Contraseña);
            if (passwordVerificationResult == PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Correo o contraseña incorrectos.";
                return View();
            }

            // Crear los claims para la autenticación
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Usuario1),
            new Claim("Correo", usuario.Correo),
        };

            // Agregar el rol (esto depende de cómo gestionas los roles)
            if (!string.IsNullOrEmpty(usuario.Roles))
            {
                claims.Add(new Claim(ClaimTypes.Role, usuario.Roles));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // Crear la cookie de autenticación
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            // Redirigir según el rol (esto lo puedes ajustar según tu lógica)
            if (usuario.Roles == "Admin")
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        // Acción para cerrar sesión
        public async Task<IActionResult> Salir()
        {
            // Cerrar sesión
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            // Redirigir al usuario a la página principal (Home)
            return RedirectToAction("Index", "AccesoController1");
        }
    }
}