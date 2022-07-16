using System.ComponentModel.DataAnnotations;
#nullable disable
namespace Mvc.Models
{
    public class OcasionModel : Model
    {
        [Display(Name = "Ocasión")]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string Ocasion { get; set; }
    }
}
