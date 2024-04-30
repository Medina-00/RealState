
using System.ComponentModel.DataAnnotations;

namespace RealState.Core.Application.ViewModel.TipoVenta
{
    public class SaveTipoVentaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ingrese el Nombre Del tipo de Venta")]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Ingrese la Decripcion Del tipo de Venta")]
        [DataType(DataType.Text)]
        public string Descripcion { get; set; }
    }
}
