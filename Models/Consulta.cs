using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class Consulta
{
    public int IdConsulta { get; set; }

    public int IdCita { get; set; }

    public string? Diagnostico { get; set; }

    public string? Tratamiento { get; set; }

    public string? Notas { get; set; }

    public DateOnly FechaConsulta { get; set; }

    public virtual Cita IdCitaNavigation { get; set; } = null!;
}
