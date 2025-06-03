
namespace ClinicaWeb.ViewModels;

public class NuevoPacienteVM
{
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Dui { get; set; } = null!;
    public DateOnly FechaNacimiento { get; set; }
    public string CorreoElectronico { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public string Direccion { get; set; } = null!;
}