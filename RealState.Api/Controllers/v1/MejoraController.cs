using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Features.mejora.Commands.CreateMejora;
using RealState.Core.Application.Features.mejora.Commands.DeleteMejoraById;
using RealState.Core.Application.Features.mejora.Commands.UpdateMejora;
using RealState.Core.Application.Features.mejora.Queries.GetAllMejora;
using RealState.Core.Application.Features.mejora.Queries.GetMejoraById;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Mantenimiento de Mejoras")]
    [Authorize(Roles = "Desarrollador,Administrador")]
    public class MejoraController : BaseApiController
    {
        
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Mejoras",
          Description = "Obtiene todas las Mejoras creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Index()
        {
            return Ok( await Mediator.Send( new GetAllMejoraQuery()));
        }
        
        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Mejora Por Id",
          Description = "Obtiene La Mejora Por El Id"
        )]

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetMejoraByIdQuery { Id = id}));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear Mejora",
          Description = "Recibe Los Parementro que se Necesita para Crear Una Mejora"
        )]

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody]CreateMejoraCommand command)
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
          Summary = "Actualizando Mejora",
          Description = "Recibe Los Parementro que se Necesita para Actualizar Una Mejora"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateMejora))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id,[FromBody] UpdateMejoraCommand command)
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
              Summary = "Eliminar una Mejora",
              Description = "Recibe los parametros necesarios para eliminar una Mejora existente"
        )]

        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {

                await Mediator.Send(new DeleteMejoraByIdCommand { Id = id });
                return NoContent();
           
        }


    }
}
