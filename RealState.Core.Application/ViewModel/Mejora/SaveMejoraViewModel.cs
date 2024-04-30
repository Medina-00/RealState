

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.Mejora
{
    public class SaveMejoraViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese el Nombre De la Mejora")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese la Decripcion De la Mejora")]
        public string Descripcion { get; set; }
    }
}
