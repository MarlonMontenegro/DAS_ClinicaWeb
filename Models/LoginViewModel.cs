using System.ComponentModel.DataAnnotations;

namespace ClinicaWeb.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Nombre_Usuario {  get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}

