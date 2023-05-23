using System.ComponentModel.DataAnnotations;

namespace Marketplace.Api.Controllers
{
    public class OfferRequest
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "La ubicación es obligatoria.")]
        public string Location { get; set; }

        [Url(ErrorMessage = "La URL de la imagen no es válida.")]
        public string PictureUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El usuario es obligatorio.")]
        public int UserId { get; set; }
        
        [Range(1, byte.MaxValue, ErrorMessage = "La categoría es obligatoria.")]
        public byte CategoryId { get; set; }
    }
}
