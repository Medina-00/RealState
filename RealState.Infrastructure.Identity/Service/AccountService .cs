

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.Account.Response;
using RealState.Core.Application.Dtos.Jwt;
using RealState.Core.Application.Enums;
using RealState.Core.Application.Interfaces;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Domain.Settings;
using RealState.Infrastructure.Identity.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RealState.Infrastructure.Identity.Service
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IEmailService emailService;
        private readonly IPropiedadRepository propiedadRepository;
        private readonly JWTSettings jWTSettings;

        public AccountService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager , IEmailService emailService 
            , IPropiedadRepository propiedadRepository, IOptions<JWTSettings> jWTSettings)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.propiedadRepository = propiedadRepository;
            this.jWTSettings = jWTSettings.Value;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request ,  bool opc)
        {
            AuthenticationResponse response = new();
            var user = await userManager.FindByEmailAsync(request.Email);
            var rol = await userManager.GetRolesAsync(user!);
            if (opc == false)
            {

                
                if (rol.Contains("Desarrollador") || rol.Contains("Administrador"))
                {
                    if (user == null)
                    {
                        response.HasError = true;
                        response.Error = $"No Existe un Cuenta registrada con el siguiente Email - {request.Email}";
                        return response;
                    }

                    var result = await signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, lockoutOnFailure: false);
                    if (!result.Succeeded)
                    {
                        response.HasError = true;
                        response.Error = $"Las Credenciales Son Incorrectas, Vuelva a Intentar.";
                        return response;
                    }
                    JwtSecurityToken jwtSecurityToken = await GenerarJWToken(user);


                    response.Id = user.Id;
                    response.Email = user.Email!;
                    response.UserName = user.UserName!;
                    response.Cedula = user.Cedula;
                    response.Foto = user.Foto;
                    var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                    response.Activo = user.Activo;
                    response.Roles = rolesList.ToList();
                    response.IsVerified = user.EmailConfirmed;
                    response.Phone = user.PhoneNumber;
                    response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                    var refreshToken = GenerarRefreshToken();
                    response.RefreshToken = refreshToken.Token;
                }
                else
                {
                    response.HasError = true;
                    response.Error = $"No Puedes Acceder a la Api, Solo Pueden Tener Acceso los Administradores o los Desarrolladores";
                    return response;

                    

                }
                
                
            }
            else
            {

                
                if (user == null)
                {
                    response.HasError = true;
                    response.Error = $"No Existe un Cuenta registrada con el siguiente Email - {request.Email}";
                    return response;
                }

                var result = await signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    response.HasError = true;
                    response.Error = $"Las Credenciales Son Incorrectas, Vuelva a Intentar.";
                    return response;
                }


                response.Id = user.Id;
                response.Email = user.Email!;
                response.UserName = user.UserName!;
                response.Cedula = user.Cedula;
                var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
                response.Activo = user.Activo;
                response.Roles = rolesList.ToList();
                response.IsVerified = user.EmailConfirmed;
            }

            



            return response;
        }

        public async Task SignOutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin)
        {
            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserNameExistente = await userManager.FindByNameAsync(request.UserName);
            if (UserNameExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Username, Ingrese uno Diferente.";
                return response;
            }

            var EmailExistente = await userManager.FindByEmailAsync(request.Email);
            if (EmailExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Email, Ingrese uno Diferente.";
                return response;
            }

            var user = new ApplicationUser
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,
                Cedula = request.Cedula,
                Foto = request.Foto,
                Activo = false,
                PhoneNumber = request.Phone
            };

            var result = await userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                
                if (request.Rol == "Cliente")
                {
                    await userManager.AddToRoleAsync(user, Roles.Cliente.ToString());
                    var verificationUri = await SendVerificationEmailUri(user, origin);
                    await emailService.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                    {
                        To = user.Email,
                        Body = $"To activate your account visit the following URL {verificationUri}",
                        Subject = "Activate your account"
                    });



                }
                else if (request.Rol == "Agente")
                {
                    await userManager.AddToRoleAsync(user, Roles.Agente.ToString());


                }

            }
            else
            {
                response.HasError = true;
                response.Error = $"Ocurrio Un Error Mientras Se Creaba El Usuario.";
                return response;
            }

            return response;
        }

        public async Task<RegisterResponse> RegisterUserAdminAsync(RegisterRequestAdmin request , string UserId , bool opc)
        {

            RegisterResponse response = new()
            {
                HasError = false
            };

            var UserNameExistente = await userManager.FindByNameAsync(request.UserName);
            if (UserNameExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Username, Ingrese uno Diferente.";
                return response;
            }

            var EmailExistente = await userManager.FindByEmailAsync(request.Email);
            if (EmailExistente != null)
            {
                response.HasError = true;
                response.Error = $"Ya esta en uso este Email, Ingrese uno Diferente.";
                return response;
            }

            var user = new ApplicationUser
            {
                Nombre = request.Nombre,
                Apellido = request.Apellido,
                Email = request.Email,
                UserName = request.UserName,
                EmailConfirmed = true,
                Cedula = request.Cedula,
                Activo = true,
            };

            if(opc)
            {
                var result = await userManager.FindByNameAsync(UserId!);
                var rol = await userManager.GetRolesAsync(result!);
                if (result != null)
                {
                    if (rol.Contains("Administrador"))
                    {
                        if (request.Rol == "Administrador")
                        {
                            await userManager.CreateAsync(user, request.Password);
                            await userManager.AddToRoleAsync(user, Roles.Administrador.ToString());
                        }
                        else if (request.Rol == "Desarrollador")
                        {
                            await userManager.CreateAsync(user, request.Password);
                            await userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
                        }



                    }
                    else if (rol.Contains("Desarrollador"))
                    {
                        if (request.Rol == "Desarrollador")
                        {
                            await userManager.CreateAsync(user, request.Password);
                            await userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
                        }
                        else
                        {
                            response.HasError = true;
                            response.Error = $"Estas Intetentando crear un Administrador  no tienes permiso para esto , o Ingrese bien el Rol de Desarrollador";
                            return response;
                        }

                    }


                }
                else
                {
                    response.HasError = true;
                    response.Error = $"Ocurrio Un Error Mientra Se Creaba El Usuario.";
                    return response;
                }
            }
            else
            {
               
                
                        if (request.Rol == "Administrador")
                        {
                            await userManager.CreateAsync(user, request.Password);
                            await userManager.AddToRoleAsync(user, Roles.Administrador.ToString());
                        }
                        else if (request.Rol == "Desarrollador")
                        {
                            await userManager.CreateAsync(user, request.Password);
                            await userManager.AddToRoleAsync(user, Roles.Desarrollador.ToString());
                        }



                else
                {
                    response.HasError = true;
                    response.Error = $"Ocurrio Un Error Mientra Se Creaba El Usuario.";
                    return response;
                }
            }
            

            return response;
        }
        public async Task LogOut()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UpdateUserViewModel> GetByUserId(string UserId)
        {
            UpdateUserViewModel update = new();
            var result = await userManager.FindByIdAsync(UserId);

            if (result == null)
            {
                update.HasError = true;
                update.Error = $"No se Encontro el usuario Con El Siguiente Id - {UserId}";
                return update;
            }

            update.UserId = result.Id;
            update.Nombre = result.Nombre!;
            update.Apellido = result.Apellido!;
            update.UserName = result.UserName!;
            update.Email = result.Email!;
            update.Cedula = result.Cedula;
            update.Foto = result.Foto!;
            update.Phone = result.PhoneNumber;

            var roles = await userManager.GetRolesAsync(result);
            update.Rol = string.Join(",", roles);


            return update;
        }

        public async Task<UpdateResponse> EditUserAsync(string UserId, UpdateRequest request)
        {
            UpdateResponse updateResponse = new();
            var user = await userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"Lo Siento, No Encontre la Cuenta Con el Siguiente Id {user!.Id}";

                return updateResponse;
            }

            user.Id = request.UserId;
            user.Nombre = request.Nombre;
            user.Apellido = request.Apellido;
            user.PhoneNumber = request.Phone;
            user.Foto = request.Foto;
            user.Cedula = request.Cedula;


            var token = await userManager.GeneratePasswordResetTokenAsync(user!);

            if (request.Password != null)
            {
                var result = await userManager.ResetPasswordAsync(user, token, request.Password!);
                if (!result.Succeeded)
                {
                    updateResponse.HasError = true;
                    updateResponse.Error = $"LO SIENTO,HA OCURRIDO UN ERROR MIENTRA SE CAMBIABA LA CONTRASEÑA!! ";

                    return updateResponse;
                }
            }


            var resultUpdate = await userManager.UpdateAsync(user);
            if (!resultUpdate.Succeeded)
            {
                updateResponse.HasError = true;
                updateResponse.Error = $"LO SIENTO,HA OCURRIDO UN ERROR MIENTRA SE ACTUALIZABA EL USER!! ";

                return updateResponse;
            }

            return updateResponse;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUser()
        {
            var result = await userManager.Users.ToListAsync();
            var propiedads = await propiedadRepository.GetAllAsync();
            var Users = new List<UserViewModel>();

            foreach (var user in result)
            {
                var roles = await userManager.GetRolesAsync(user);
                var cantidad =  propiedads.Where( p => p.UserId == user.Id ).Count();
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    Nombre = user.Nombre!,
                    Apellido = user.Apellido!,
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Rol = string.Join(",", roles),
                    Foto = user.Foto!,
                    Cedula = user.Cedula!,
                    Activo = user.Activo!,
                    Phone = user.PhoneNumber,
                    CantidadPropiedad = cantidad
                };

                Users.Add(userViewModel);
            }

            return Users;
        }

        public async Task<bool> Activar(string UserId, ActivarUser activarUser)
        {

            var user = await userManager.FindByIdAsync(UserId);

            if (activarUser.Activo == "Activar")
            {
                user!.Activo = true;
                await userManager.UpdateAsync(user);
                return true;
            }
            else
            {
                user!.Activo = false;
                await userManager.UpdateAsync(user);
                return false;
            }




            
        }

        public async Task<String> ActivarNewAccount(string userId,  ActivarUser activarUser)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return "LO SIENTO , NO ENCONTRAMOS EL USUARIO";
            }
            
            
            if (await Activar(userId, activarUser))
            {
                return $"SU CUENTA A SIDO ACTIVADA , YA PERTENECES A NUESTRA RED SOCIAL";
            }
            else
            {
                return "HA OCURRIDO UN ERROR MIENTRA EL PROCESO DE ACTIVAR SU CUENTA";
            }

        }
        private async Task<string> SendVerificationEmailUri(ApplicationUser user, string origin)
        {
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ActivarNewAccount";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUri = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUri = QueryHelpers.AddQueryString(verificationUri, "token", code);

            return verificationUri;
        }

        #region JWT
        private async Task<JwtSecurityToken> GenerarJWToken(ApplicationUser user)
        {
            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            //AQUI AGREGOS LOS ROLES DEL USUARIO A UNA LISTA
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }


            //AQUI CREAMOS UN CLAIMS 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim("Id", user.Id)
            }
            // aqui hago union entre los claims creados y los claims que trae el usuario
            .Union(userClaims)
            .Union(roleClaims);

            var LlaveSimetricaSeguridad = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jWTSettings.Key));
            var Credenciales = new SigningCredentials(LlaveSimetricaSeguridad, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jWTSettings.Issuer,
                audience: jWTSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(jWTSettings.DurationInMinutes),
                signingCredentials: Credenciales);

            return jwtSecurityToken;
        }

        private RefreshToken GenerarRefreshToken()
        {
            return new RefreshToken
            {
                //aqui genero el token.
                Token = RandomTokenString(),
                //aqui le doy la vigencia al token.
                Expires = DateTime.UtcNow.AddDays(5),
                Created = DateTime.UtcNow
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var ramdomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(ramdomBytes);

            //aqui convierto y le doy formato al token.
            return BitConverter.ToString(ramdomBytes).Replace("-", "");
        }



        #endregion


    }


}
