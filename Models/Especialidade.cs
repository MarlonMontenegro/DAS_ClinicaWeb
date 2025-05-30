using System;
using System.Collections.Generic;

namespace ClinicaWeb.Models;

public partial class Especialidade
{
    public int Id { get; set; }

    public string NombreEspecialidad { get; set; } = null!;

    public virtual ICollection<Medico> Medicos { get; set; } = new List<Medico>();
}
