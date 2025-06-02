using ClinicaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ClinicaWeb.Controllers
{
    public class CitaController : Controller
    {
        private readonly ClinicaDbContext _context;

        public CitaController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Cita/Agendar
        public IActionResult Agendar()
        {
            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .Select(p => new { p.Id, NombreCompleto = p.Nombre + " " + p.Apellido }),
                "Id", "NombreCompleto");

            ViewBag.Medicos = new SelectList(_context.Medicos
                .Include(m => m.IdEspecialidadNavigation)
                .Select(m => new {
                    m.Id,
                    NombreCompleto = m.Nombre + " " + m.Apellido + " - " + m.IdEspecialidadNavigation.NombreEspecialidad
                }),
                "Id", "NombreCompleto");

            return View();
        }

        // POST: Cita/Agendar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Agendar(Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Citas.Add(cita);
                _context.SaveChanges();
                return RedirectToAction("Dashboard", "Home");
            }

            ViewBag.Pacientes = new SelectList(_context.Pacientes
                .Select(p => new { p.Id, NombreCompleto = p.Nombre + " " + p.Apellido }),
                "Id", "NombreCompleto", cita.IdPaciente);

            ViewBag.Medicos = new SelectList(_context.Medicos
                .Include(m => m.IdEspecialidadNavigation)
                .Select(m => new {
                    m.Id,
                    NombreCompleto = m.Nombre + " " + m.Apellido + " - " + m.IdEspecialidadNavigation.NombreEspecialidad
                }),
                "Id", "NombreCompleto", cita.IdMedico);

            return View(cita);
        }
    }
}

