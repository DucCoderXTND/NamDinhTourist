using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.RoomClasses.Create
{
    public class CreateRoomClassCommandHandler : IRequestHandler<CreateRoomClassCommand, Guid>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRepository _hotelRepository;
        private readonly IAmenityRepository _amenityRepository;

        public CreateRoomClassCommandHandler(IRoomClassRepository roomClassRepository,
            IMapper mapper, IUnitOfWork unitOfWork, IHotelRepository hotelRepository,
            IAmenityRepository amenityRepository)
        {
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hotelRepository = hotelRepository;
            _amenityRepository = amenityRepository;
        }
        public async Task<Guid> Handle(CreateRoomClassCommand request, 
            CancellationToken cancellationToken = default)
        {
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId))
                throw new NotFoundException(HotelMessages.NotFound);

            if (await _roomClassRepository.ExistsByNameInHotelAsync(request.HotelId, request.Name, cancellationToken))
                throw new RoomClassWithSameNameFoundException(RoomClassMessages.NameInHotelFound);

            foreach(var amenityId in request.AmenitiesIds)
            {
                if (!await _amenityRepository.ExistsByIdAsync(amenityId))
                    throw new NotFoundException(AmenityMessages.WithIdNotFound);
            }

            RoomClass roomClass = _mapper.Map<RoomClass>(request);
            foreach(var amenityId in request.AmenitiesIds)
            {
                roomClass.Amenities.Add((await _amenityRepository.GetByIdAsync(amenityId, cancellationToken))!);
            }

            var createRoomClass = await _roomClassRepository.CreateAsync(roomClass, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return createRoomClass.Id;
        }
    }
}
