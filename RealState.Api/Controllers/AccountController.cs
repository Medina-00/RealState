using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Features.mejora.Commands.CreateMejora;
using RealState.Core.Application.Features.mejora.Queries.GetAllMejora;
using RealState.Core.Application.Features.mejora.Queries.GetMejoraById;
using RealState.Core.Application.Features.Propiedad.Queries.GetPropiedadesByUser;
using RealState.Core.Application.Features.Tipopropiedad.Commands.UpdateTipoPropiedad;
using RealState.Core.Application.Features.Usuario.Commands.ActivarUsuario;
using RealState.Core.Application.Features.Usuario.Queries.GetAllUsuario;
using RealState.Core.Application.Features.Usuario.Queries.GetUsuarioById;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.ViewModel.User;
using RealState.Infrastructure.Identity.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;
using System.Security.Claims;

namespace RealState.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Mantenimiento de Cuentas(Usarios)")]

    public class AccountController : BaseApiController
    {
        private readonly IAccountService accountService;


        public AccountController(IAccountService accountService )
        {
            this.accountService = accountService;
        }

        [HttpPost("Login")]
        [SwaggerOperation(
         Summary = "Login de usuario",
         Description = "Autentica un usuario en el sistema y le retorna un JWT"
     )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            return Ok(await accountService.AuthenticateAsync(request,false));
        }

        [Authorize]
        [HttpPost("Regitrar")]
        [SwaggerOperation(
            Summary = "Creacion de usuario basico",
            Description = "Recibe los parametros necesarios para crear un usuario con el rol basico"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Registrarse(RegisterRequestAdmin request)
        {
            // Aquí obtienes el UserName del usuario autenticado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            //aqui lo paso y hagos las configuraciones de crecion dependiendo en rol de usurio, en el accountService
            return Ok(await accountService.RegisterUserAdminAsync(request, userId!,true));
        }

        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Usuarios",
          Description = "Obtiene todos los Usuarios creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Index()
        {
            return Ok(await Mediator.Send(new GetAllUsuarioQuery()));
        }

        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet("{id}")]
        [SwaggerOperation(
          Summary = "Usuario Por Id",
          Description = "Obtiene el Usuario Por El Id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateUserViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Get(string id)
        {
            return Ok(await Mediator.Send(new GetUsuarioByIdQuery { UserId = id }));
        }


        
        [Authorize(Roles = "Desarrollador,Administrador")]
        [HttpGet("Propiedades")]
        [SwaggerOperation(
          Summary = "Propiedades Por el Id del usuario",
          Description = "Obtiene las Propiedades Por El Id del usuario"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadReponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        // GET: PropiedadController/Details/5
        public async Task<ActionResult> Get([FromQuery] GetPropiedadesByUserQuery parameter)
        {
            return Ok(await Mediator.Send(new GetPropiedadesByUserQuery() { UserId = parameter.UserId }));
        }


        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        [SwaggerOperation(
          Summary = "Activar de usuario",
          Description = "Permite activar un usuario nuevo u Existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(string id, ActivarUsuarioCommand command)
        {
           
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                if (id != command.UserId)
                {
                    return BadRequest();
                }

                return Ok(await Mediator.Send(command));
           
        }

    }
}
