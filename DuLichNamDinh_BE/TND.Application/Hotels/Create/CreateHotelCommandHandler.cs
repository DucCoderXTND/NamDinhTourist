using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.Create
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, Guid>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICityRepository _cityRepository;

        public CreateHotelCommandHandler(IHotelRepository hotelRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            IOwnerRepository ownerRepository,
            ICityRepository cityRepository)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _ownerRepository = ownerRepository;
            _cityRepository = cityRepository;
        }
        public async Task<Guid> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            if (!await _cityRepository.ExistsByIdAsync(request.CityId))
            {
                throw new NotFoundException(CityMessages.NotFound);
            }
            if (!await _ownerRepository.ExistsByIdAsync(request.OwnerId))
            {
                throw new NotFoundException(OwnerMessages.NotFound);
            }
            if (await _hotelRepository.ExistsByLocation(request.Longitude, request.Latitude))
            {
                throw new HotelLocationReplicationException(HotelMessages.SameLocationExists);
            }
            var cratedHotel = await _hotelRepository.CreateAsync(
                _mapper.Map<Hotel>(request), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return cratedHotel.Id;
        }
    }
}
