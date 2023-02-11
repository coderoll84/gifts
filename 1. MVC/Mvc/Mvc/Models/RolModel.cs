using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Mvc.Models
{
    public class RolModel : Model
    {
        [Display(Name = "Rol")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Descripcion { get; set; }
    }
}
