

using AutoMapper;
using RealState.Core.Application.Dtos.Account.Request;
using RealState.Core.Application.Dtos.mejora;
using RealState.Core.Application.Dtos.Propiedad;
using RealState.Core.Application.Dtos.TipoPropiedad;
using RealState.Core.Application.Dtos.Tipoventa;
using RealState.Core.Application.Features.mejora.Commands.CreateMejora;
using RealState.Core.Application.Features.mejora.Commands.UpdateMejora;
using RealState.Core.Application.Features.Tipopropiedad.Commands.CreateTipoPropiedad;
using RealState.Core.Application.Features.Tipopropiedad.Commands.UpdateTipoPropiedad;
using RealState.Core.Application.Features.Tipoventa.Commands.CreateTipoVenta;
using RealState.Core.Application.Features.Tipoventa.Commands.UpdateTipoVenta;
using RealState.Core.Application.ViewModel.Mejora;
using RealState.Core.Application.ViewModel.Propiedad;
using RealState.Core.Application.ViewModel.PropiedadFavorita;
using RealState.Core.Application.ViewModel.PropiedadMejora;
using RealState.Core.Application.ViewModel.TipoPropiedad;
using RealState.Core.Application.ViewModel.TipoVenta;
using RealState.Core.Application.ViewModel.User;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ForMember(d => d.File, o => o.Ignore())
                .ReverseMap();


            CreateMap<RegisterRequestAdmin, SaveUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ForMember(d => d.File, o => o.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UpdateUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<TipoVenta, TipoVentaViewModel>()

               .ReverseMap()
                .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
            

               .ReverseMap()
               .ForMember(d => d.Cantidad, o => o.Ignore())
                .ForMember(d => d.Propiedades, o => o.Ignore());


            CreateMap<TipoPropiedad, TipoPropiedadViewModel>()

               .ReverseMap()
                .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
             

               .ReverseMap()
               .ForMember(d => d.Cantidad, o => o.Ignore())
                .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<TipoPropiedad, TipoPropiedadResponse>()

              .ReverseMap()
               .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<CreateTipoPropiedadCommand, TipoPropiedad>()


              .ForMember(d => d.Id, o => o.Ignore())
              .ForMember(d => d.Cantidad, o => o.Ignore())
               .ForMember(d => d.Propiedades, o => o.Ignore())
              .ReverseMap();

            CreateMap<UpdateTipoPropiedadCommand, TipoPropiedad>()

             .ForMember(d => d.Cantidad, o => o.Ignore())
              .ForMember(d => d.Propiedades, o => o.Ignore())

             .ReverseMap();

            CreateMap<UpdateTipoPropiedad, TipoPropiedad>()

             .ForMember(d => d.Cantidad, o => o.Ignore())
              .ForMember(d => d.Propiedades, o => o.Ignore())

             .ReverseMap();

            CreateMap<TipoVenta, TipoVentaResponse>()

              .ReverseMap()
               .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<CreateTipoVentaCommand, TipoVenta>()


              .ForMember(d => d.Id, o => o.Ignore())
              .ForMember(d => d.Cantidad, o => o.Ignore())
               .ForMember(d => d.Propiedades, o => o.Ignore())
              .ReverseMap();

            CreateMap<UpdateTipoVentaCommand, TipoVenta>()

             .ForMember(d => d.Cantidad, o => o.Ignore())
              .ForMember(d => d.Propiedades, o => o.Ignore())

             .ReverseMap();

            CreateMap<UpdateTipoVenta, TipoVenta>()

             .ForMember(d => d.Cantidad, o => o.Ignore())
              .ForMember(d => d.Propiedades, o => o.Ignore())

             .ReverseMap();

            CreateMap<Mejora, MejoraViewModel>()
            

               .ReverseMap()
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());

            CreateMap<Mejora, SaveMejoraViewModel>()


               .ReverseMap()
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());


            CreateMap<Mejora, MejoraResponse>()


               .ReverseMap()
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());

            CreateMap<CreateMejoraCommand, Mejora>()

                .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore())

               .ReverseMap();

            CreateMap<UpdateMejoraCommand, Mejora>()

                .ForMember(d => d.propiedadMejoras, o => o.Ignore())

               .ReverseMap();

            CreateMap<UpdateMejora, Mejora>()

               .ForMember(d => d.propiedadMejoras, o => o.Ignore())

              .ReverseMap();

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()


               .ReverseMap()
               .ForMember(d => d.Id, o => o.Ignore())
                .ForMember(d => d.Propiedades, o => o.Ignore());

            CreateMap<Propiedad, PropiedadViewModel>()


               .ReverseMap()
               .ForMember(d => d.TipoVenta, o => o.Ignore())
                .ForMember(d => d.TipoPropiedad, o => o.Ignore())
                 .ForMember(d => d.PropiedadFavoritas, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());

            CreateMap<Propiedad, SavePropiedadViewModel>()
                 .ForMember(d => d.Mejoras, o => o.Ignore())

                .ForMember(d => d.TipoPropiedad, o => o.Ignore())
                .ForMember(d => d.TipoVenta, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore())
                .ForMember(d => d.FilePrincipal, o => o.Ignore())
                .ForMember(d => d.File1, o => o.Ignore())
                .ForMember(d => d.File2, o => o.Ignore())
                .ForMember(d => d.File3, o => o.Ignore())
           .ReverseMap()
           .ForMember(d => d.PropiedadFavoritas, o => o.Ignore())
            .ForMember(d => d.Numero6Digitos, o => o.Ignore())
                .ForMember(d => d.Fecha, o => o.Ignore())
               .ForMember(d => d.TipoVenta, o => o.Ignore())
                .ForMember(d => d.TipoPropiedad, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());

            CreateMap<PropiedadMejora, PropiedadMejoraViewModel>()


               .ReverseMap()
                .ForMember(d => d.Mejora, o => o.Ignore())
                .ForMember(d => d.Propiedad, o => o.Ignore());

            CreateMap<PropiedadFavorita, PropiedadFavoritaViewModel>()
               .ReverseMap()
               .ForMember(d => d.Propiedad, o => o.Ignore());

            CreateMap<PropiedadFavorita, SavePropiedadFavoritaViewModel>()
               .ReverseMap()
               .ForMember(d => d.IdFavorita, o => o.Ignore())
               .ForMember(d => d.Propiedad, o => o.Ignore());


            CreateMap<Propiedad, PropiedadReponse>()
               
                 .ForMember(d => d.Mejoras, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore())

               .ReverseMap()
              
               .ForMember(d => d.TipoVenta, o => o.Ignore())
                .ForMember(d => d.TipoPropiedad, o => o.Ignore())
                 .ForMember(d => d.PropiedadFavoritas, o => o.Ignore())
                .ForMember(d => d.propiedadMejoras, o => o.Ignore());

            CreateMap<PropiedadFavorita, SavePropiedadFavoritaViewModel>()
               .ReverseMap()
               .ForMember(d => d.IdFavorita, o => o.Ignore())
               .ForMember(d => d.Propiedad, o => o.Ignore());



        }
    }
}
