using ClinicaWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace ClinicaWeb.Controllers
{
    public class MedicoController : Controller
    {
        private readonly ClinicaDbContext _context;
        private readonly IHttpContextAccessor _http;

        public MedicoController(ClinicaDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        private int? ObtenerIdMedico()
        {
            ViewBag.Rol = "Medico";
            int? idMedico = _http.HttpContext.Session.GetInt32("IdMedico");
            if (idMedico != null)
                return idMedico;

            int? idUsuario = _http.HttpContext.Session.GetInt32("IdUsuario");
            if (idUsuario == null) return null;

            idMedico = _context.UsuarioMedicos
                         .Where(um => um.IdUsuario == idUsuario)
                         .Select(um => um.IdMedico)
                         .FirstOrDefault();

            if (idMedico != null)
                _http.HttpContext.Session.SetInt32("IdMedico", idMedico.Value);

            return idMedico;
        }

        // GET: /Medico/MisCitas
        public IActionResult MisCitas()
        {
            ViewBag.Rol = "Medico";
            int? idMedico = ObtenerIdMedico();
            if (idMedico == null)
                return RedirectToAction("Login", "Account");

            var citas = _context.Citas
                .Include(c => c.IdPacienteNavigation)
                .Where(c => c.IdMedico == idMedico)
                .OrderBy(c => c.FechaHora)
                .ToList();

            return View(citas);
        }

        // GET: /Medico/Historial
        public IActionResult Historial()
        {
            ViewBag.Rol = "Medico";
            int? idMedico = ObtenerIdMedico();
            if (idMedico == null)
                return RedirectToAction("Login", "Account");

            var historiales = _context.HistorialMedicos
                .Include(h => h.IdPacienteNavigation)
                .Where(h => _context.Citas.Any(c => c.IdPaciente == h.IdPaciente && c.IdMedico == idMedico))
                .OrderByDescending(h => h.FechaRegistro)
                .ToList();

            return View(historiales);
        }
    }
}