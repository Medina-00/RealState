﻿

namespace RealState.Core.Application.Dtos.TipoPropiedad
{
    public class TipoPropiedadResponse
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public int? Cantidad { get; set; }
    }
}
