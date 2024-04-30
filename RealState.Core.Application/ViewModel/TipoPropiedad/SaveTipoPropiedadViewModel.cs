

using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.TipoPropiedad
{
    public class SaveTipoPropiedadViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre Del tipo de Propiedad")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese la Decripcion Del tipo de Propiedad")]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }
    }
}
