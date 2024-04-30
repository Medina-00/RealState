
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Features.Propiedad.Queries.GetAllPropiedad;
using RealState.Core.Application.Features.Propiedad.Queries.GetByIdPropiedad;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealState.Api.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Desarrollador,Administrador")]
    [SwaggerTag("Mantenimiento de Propiedades")]

    public class PropiedadController : BaseApiController
    {

        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Propiedades",
          Description = "Obtiene todas las Propiedades creadas"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // GET: PropiedadController
        public async Task<ActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPropiedadesQuery()));

        }



        [HttpGet("{id}")]
        [Authorize(Roles = "Desarrollador,Administrador")]
        [SwaggerOperation(
          Summary = " Propiedad Filtrada Por su Id",
          Description = "Obtiene la Propiedad Por Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // GET: PropiedadController/Details/5
        public async Task<ActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetPropiedadByIdQuery { IdPropiedad = id }));
        }


        [HttpGet("Propiedades")]
        [Authorize(Roles = "Desarrollador,Administrador")]
        [SwaggerOperation(
          Summary = "Propiedad Por Su Filtrada por su numuero",
          Description = "Obtiene las Propiedad Por Su Numero"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // GET: PropiedadController/Details/5
        public async Task<ActionResult> Get([FromQuery] GetAllPropiedadesParameter parameter)
        {
            return Ok(await Mediator.Send(new GetPropiedadesByNumeroQuery() { Numero6Digito = parameter.Numero6Digito }));
        }

       


    }
}
