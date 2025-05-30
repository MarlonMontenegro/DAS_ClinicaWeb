using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class UsuarioMedico
{
    public int IdUsuario { get; set; }

    public int? IdMedico { get; set; }

    public virtual Medico? IdMedicoNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
}
