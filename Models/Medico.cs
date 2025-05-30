using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class Medico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public int IdEspecialidad { get; set; }

    public string Dui { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? CorreoElectronico { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual Especialidade IdEspecialidadNavigation { get; set; } = null!;

    public virtual UsuarioMedico? UsuarioMedico { get; set; }
}
