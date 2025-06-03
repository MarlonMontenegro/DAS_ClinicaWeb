using ClinicaWeb.Models;
using ClinicaWeb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly ClinicaDbContext _context;

        public AdminController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: /Admin/Lista
        public IActionResult Lista()
        {
            ViewBag.Rol = "Administrador";
            var vm = new UsuariosAgrupadosVM
            {
                Administradores = _context.Usuarios
                    .Where(u => u.Rol == "Administrador")
                    .ToList(),

                Recepcionistas = _context.Usuarios
                    .Where(u => u.Rol == "Recepcionista")
                    .ToList(),

                Medicos = _context.Medicos
                    .Include(m => m.IdEspecialidadNavigation)
                    .ToList(),

                Pacientes = _context.Pacientes.ToList()
            };

            return View(vm);           
        }
        public IActionResult Configuracion()
        {
            ViewBag.Rol = "Administrador";
            ViewBag.Especialidades = new SelectList(_context.Especialidades,
                "Id", "NombreEspecialidad");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CrearUsuario(NuevoUsuarioVM vm)
        {
            ViewBag.Rol = "Administrador";
            if (!ModelState.IsValid)
            {
                ViewBag.Especialidades = new SelectList(_context.Especialidades,
                    "Id", "NombreEspecialidad", vm.IdEspecialidad);
                return View("Configuracion", vm);
            }

            
            var usuario = new Usuario
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                NombreUsuario = vm.NombreUsuario,
                Contrasena = vm.Contrasena,
                Rol = vm.Rol.ToString(),
                CorreoElectronico = vm.CorreoElectronico
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            if (vm.Rol == TipoRol.Medico)
            {
                
                var medico = new Medico
                {
                    Nombre = vm.Nombre,
                    Apellido = vm.Apellido,
                    Dui = vm.Dui,
                    IdEspecialidad = vm.IdEspecialidad ?? 0,
                    CorreoElectronico = vm.CorreoElectronico
                };
                _context.Medicos.Add(medico);
                _context.SaveChanges();

                
                _context.UsuarioMedicos.Add(new UsuarioMedico
                {
                    IdUsuario = usuario.IdUsuario,
                    IdMedico = medico.Id
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Lista"); 
        }
        [HttpPost]
        public IActionResult EliminarUsuario(int id)
        {
            ViewBag.Rol = "Administrador";
            var u = _context.Usuarios.Find(id);
            if (u != null)
            {
                _context.Usuarios.Remove(u);
                _context.SaveChanges();
            }
            return RedirectToAction("Lista");
        }

        [HttpPost]
        public IActionResult EliminarMedico(int id)
        {
            ViewBag.Rol = "Administrador";
            var m = _context.Medicos.Find(id);
            if (m != null)
            {
                _context.Medicos.Remove(m);
                _context.SaveChanges();
            }
            return RedirectToAction("Lista");
        }

        [HttpPost]
        public IActionResult EliminarPaciente(int id)
        {
            ViewBag.Rol = "Administrador";
            var p = _context.Pacientes.Find(id);
            if (p != null)
            {
                _context.Pacientes.Remove(p);
                _context.SaveChanges();
            }
            return RedirectToAction("Lista");
        }
    }
}