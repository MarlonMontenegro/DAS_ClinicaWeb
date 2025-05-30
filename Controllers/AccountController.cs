using ClinicaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClinicaWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ClinicaDbContext _context;



        public AccountController(ClinicaDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NombreUsuario == model.Nombre_Usuario && u.Contrasena == model.Password);

            if (usuario != null)
            {
                return RedirectToAction("Dashboard", new { rol = usuario.Rol });
            }

            ModelState.AddModelError("", "Usuario o contraseña incorrectos.");
            return View(model);
        }

        public IActionResult Dashboard(string rol)
        {
            ViewBag.Rol = rol;
            return View();
        }

    }
}
