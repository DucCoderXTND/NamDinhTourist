using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.Update
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHotelCommandHandler(IHotelRepository hotelRepository,
            IOwnerRepository ownerRepository, ICityRepository cityRepository, IMapper mapper,
            IUnitOfWork unitOfWork)
        {
            _hotelRepository = hotelRepository;
            _ownerRepository = ownerRepository;
            _cityRepository = cityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken = default)
        {
            var hotelEntity = await _hotelRepository.GetByIdAsync(
             request.HotelId,
             false, false, false,
             cancellationToken) ?? throw new NotFoundException(HotelMessages.NotFound);

            if(!await _cityRepository.ExistsByIdAsync(request.CityId))
                throw new NotFoundException(CityMessages.NotFound);

            if(!await _ownerRepository.ExistsByIdAsync(request.OwnerId))
                throw new NotFoundException(OwnerMessages.NotFound);

            if (await _hotelRepository.ExistsByLocation(request.Longitude, request.Latitude))
                throw new HotelLocationReplicationException(HotelMessages.SameLocationExists);

            _mapper.Map(request, hotelEntity);

            await _hotelRepository.UpdateAsync(hotelEntity, cancellationToken); 
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
