namespace ClinicaWeb.ViewModels;

using ClinicaWeb.Models;
using System.Collections.Generic;

public class UsuariosAgrupadosVM
{
    public IEnumerable<Usuario> Administradores { get; set; } = new List<Usuario>();
    public IEnumerable<Usuario> Recepcionistas { get; set; } = new List<Usuario>();
    public IEnumerable<Medico> Medicos { get; set; } = new List<Medico>();
    public IEnumerable<Paciente> Pacientes { get; set; } = new List<Paciente>();
}