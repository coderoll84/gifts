using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Mvc.Models
{
    public class UsuarioModel : Model
    {

        public string Descripcion { get; set; }

        [Display(Name = "Login")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Login { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "Dirección de correo electronico invalida.")]
        public string Email { get; set; }

        [Display(Name = "Cel.")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [RegularExpression(@"^[0-9]{10,10}$", ErrorMessage = "{0} invalido")]
        public string Telefono { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Compare("Password", ErrorMessage = "La contraseña y su confirmación deben coincidir.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
