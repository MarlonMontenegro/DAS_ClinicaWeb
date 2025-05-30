using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string NombreUsuario { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public string? CorreoElectronico { get; set; }

    public virtual UsuarioMedico? UsuarioMedico { get; set; }
}
