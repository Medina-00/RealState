
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Features.Tipopropiedad.Commands.CreateTipoPropiedad;
using RealState.Core.Application.Features.Tipopropiedad.Commands.DeleteTipoPropiedadById;
using RealState.Core.Application.Features.Tipopropiedad.Commands.UpdateTipoPropiedad;
using RealState.Core.Application.Features.Tipopropiedad.Queries.GetAllTipoPropiedades;
using RealState.Core.Application.Features.Tipopropiedad.Queries.GetTipoPropiedadesById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Tipo de Propiedades")]

    public class TipoPropiedadController : BaseApiController
    {
        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Tipo de Propiedad",
          Description = "Obtiene todos los Tipo Propiedad creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Index()
        {
            return Ok(await Mediator.Send(new GetAllTipoPropiedadesQuery()));
        }



        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Tipo Propiedad Por Id",
          Description = "Obtiene el Tipo de Propiedad Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id }));
        }



        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear tipo De Propiedad",
          Description = "Recibe Los Parementro que se Necesita para Crear Un Tipo De Propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateTipoPropiedadCommand command)
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
          Summary = "Actualizando Tipo de Propiedad",
          Description = "Recibe Los Parementro que se Necesita para Actualizar un Tipo de Propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTipoPropiedad))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateTipoPropiedadCommand command)
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
              Summary = "Eliminar un Tipo De Propiedad",
              Description = "Recibe los parametros necesarios para eliminar un Tipo de Propiedad existente, al eliminar este Tipo Propiedad " +
              "se elimina las Propiedades asociada con esta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            
                await Mediator.Send(new DeleteTipoPropiedadByIdCommand { Id = id });
                return NoContent();
            
        }

    }
}
