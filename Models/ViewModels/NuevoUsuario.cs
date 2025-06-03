namespace ClinicaWeb.ViewModels;

public enum TipoRol { Medico, Recepcionista }

public class NuevoUsuarioVM
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string NombreUsuario { get; set; } = null!;
    public string Contrasena { get; set; } = null!;
    public string CorreoElectronico { get; set; } = null!;
    public TipoRol Rol { get; set; }

    
    public string? Dui { get; set; }
    public int? IdEspecialidad { get; set; }
}