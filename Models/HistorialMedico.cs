using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class HistorialMedico
{
    public int Id { get; set; }

    public int IdPaciente { get; set; }

    public string Tipo { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime? FechaRegistro { get; set; }

    public virtual Paciente IdPacienteNavigation { get; set; } = null!;
}
