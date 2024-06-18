using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Cities.Update
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCityCommandHandler(ICityRepository cityRepository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            if(await _cityRepository.ExistsByPostOfficeAsync(request.PostOffice, cancellationToken))
            {
                throw new CityWithPostOfficeExistsException(CityMessages.PostOfficeExists);
            }

            var cityEntity = await _cityRepository.GetByIdAsync(request.CityId)
                ?? throw new NotFoundException(CityMessages.NotFound);

            _mapper.Map(request, cityEntity);

            await _cityRepository.UpdateAsync(cityEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
