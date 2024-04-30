
using AutoMapper;
using RealState.Core.Application.Interfaces.Repositories;
using RealState.Core.Application.Interfaces.Services;
using RealState.Core.Application.ViewModel.Propiedad;
using RealState.Core.Domain.Entities;

namespace RealState.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel , PropiedadViewModel,Propiedad> ,IPropiedadService
    {
        private readonly IPropiedadRepository propiedadRepository;
        private readonly IMapper mapper;

        public PropiedadService( IPropiedadRepository propiedadRepository,IMapper mapper) :base(propiedadRepository ,mapper)
        {
            this.propiedadRepository = propiedadRepository;
            this.mapper = mapper;
        }

        public override async Task<SavePropiedadViewModel> Add(SavePropiedadViewModel vm)
        {
            Propiedad entity = mapper.Map<Propiedad>(vm);

            entity.propiedadMejoras = entity.propiedadMejoras ?? new List<PropiedadMejora>();

            if (vm.propiedadMejoras != null)
            {
                foreach (var item in vm.propiedadMejoras)
                {
                    PropiedadMejora propiedadMejora = new PropiedadMejora();
                    propiedadMejora.IdMejora = item;

                    

                    entity.propiedadMejoras.Add(propiedadMejora);
                }
            }


            entity = await propiedadRepository.AddAsync(entity);

            SavePropiedadViewModel entityVm = mapper.Map<SavePropiedadViewModel>(entity);

            return entityVm;
        }

        public override async Task Update(SavePropiedadViewModel vm, int id)
        {
            Propiedad entity = mapper.Map<Propiedad>(vm);
            entity.propiedadMejoras = entity.propiedadMejoras ?? new List<PropiedadMejora>();

            if (vm.propiedadMejoras != null)
            {
                foreach (var item in vm.propiedadMejoras)
                {
                    PropiedadMejora propiedadMejora = new PropiedadMejora();
                    propiedadMejora.IdMejora = item;



                    entity.propiedadMejoras.Add(propiedadMejora);
                }
            }
            await propiedadRepository.UpdateAsync(entity, id);
        }
    }
}
