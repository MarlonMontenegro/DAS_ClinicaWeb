using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class Cita
{
    public int IdCita { get; set; }

    public int IdPaciente { get; set; }

    public int IdMedico { get; set; }

    public DateTime FechaHora { get; set; }

    public string? MotivoConsulta { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<Consulta> Consulta { get; set; } = new List<Consulta>();

    public virtual Medico IdMedicoNavigation { get; set; } = null!;

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
