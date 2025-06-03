using ClinicaWeb.Models;                    
using ClinicaWeb.ViewModels;                
using Microsoft.AspNetCore.Mvc;             
using Microsoft.EntityFrameworkCore;        
namespace ClinicaWeb.Controllers             
{
    public class PacienteController : Controller
    {
        private readonly ClinicaDbContext _context;

        public PacienteController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: /Paciente/Crear
        public IActionResult Crear()
        {
            ViewBag.Rol = "Recepcionista";
            return View();                  
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Crear(NuevoPacienteVM vm)
        {
            ViewBag.Rol = "Recepcionista";
            if (!ModelState.IsValid)
                return View(vm);

            var paciente = new Paciente
            {
                Nombre = vm.Nombre,
                Apellido = vm.Apellido,
                Dui = vm.Dui,
                FechaNacimiento = vm.FechaNacimiento,
                CorreoElectronico = vm.CorreoElectronico,
                Telefono = vm.Telefono,
                Direccion = vm.Direccion
            };

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return RedirectToAction("Dashboard", "Account");
        }
    }
}