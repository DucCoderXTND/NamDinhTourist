using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Cities.Create
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityResponse>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCityCommandHandler(ICityRepository cityRepository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<CityResponse> Handle(CreateCityCommand request, 
            CancellationToken cancellationToken = default)
        {
            if(await _cityRepository.ExistsByPostOfficeAsync(request.PostOffice, cancellationToken))
            {
                throw new CityWithPostOfficeExistsException(CityMessages.PostOfficeExists);
            }

            var createdCity = await _cityRepository.CreateAsync(
                _mapper.Map<City>(request), cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<CityResponse>(createdCity);
        }
    }
}
