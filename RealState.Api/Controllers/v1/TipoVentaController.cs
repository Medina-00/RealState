using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Dtos.Tipoventa;
using RealState.Core.Application.Features.Tipopropiedad.Queries.GetTipoPropiedadesById;
using RealState.Core.Application.Features.Tipoventa.Commands.CreateTipoVenta;
using RealState.Core.Application.Features.Tipoventa.Commands.DeleteTipoVentaById;
using RealState.Core.Application.Features.Tipoventa.Commands.UpdateTipoVenta;
using RealState.Core.Application.Features.Tipoventa.Queries.GetAllTipoVenta;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipos de Ventas")]

    public class TipoVentaController : BaseApiController
    {
        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Tipo de Venta",
          Description = "Obtiene todos los Tipos de Ventas creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Index()
        {
            return Ok(await Mediator.Send(new GetAllTipoVentaQuery()));
        }


        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Tipo Venta Por Id",
          Description = "Obtiene el Tipo de Venta Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id }));
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear tipo De Venta",
          Description = "Recibe Los Parementro que se Necesita para Crear Un Tipo De Venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTipoVentaCommand command)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await Mediator.Send(command);
            return NoContent();

        }
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        [SwaggerOperation(
          Summary = "Actualizando Tipo de Venta",
          Description = "Recibe Los Parementro que se Necesita para Actualizar un Tipo de Venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTipoPropiedad))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateTipoVentaCommand command)
        {
            
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.Id)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
            
        }

        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        [SwaggerOperation(
              Summary = "Eliminar un Tipo De Venta",
              Description = "Recibe los parametros necesarios para eliminar un Tipo de Venta existente, al eliminar este Tipo Venta " +
              "se elimina las Propiedades asociada con esta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
           
                await Mediator.Send(new DeleteTipoVentaByIdCommand { Id = id });
                return NoContent();
           
        }

    }
}
