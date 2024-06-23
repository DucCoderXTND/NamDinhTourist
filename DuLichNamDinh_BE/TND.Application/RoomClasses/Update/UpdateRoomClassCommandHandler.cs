using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.RoomClasses.Update
{
    public class UpdateRoomClassCommandHandler : IRequestHandler<UpdateRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomClassCommandHandler(IRoomClassRepository roomClassRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateRoomClassCommand request, CancellationToken cancellationToken)
        {
            var roomClass = await _roomClassRepository
                .GetByIdAsync(request.RoomClassId, cancellationToken)
                ?? throw new NotFoundException(RoomClassMessages.NotFound);

            if (await _roomClassRepository.ExistsByNameInHotelAsync(roomClass.HotelId, request.Name, cancellationToken))
                throw new RoomClassWithSameNameFoundException(RoomClassMessages.NameInHotelFound);

            _mapper.Map(request, roomClass);

            await _roomClassRepository.UpdateAsync(roomClass, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        }
    }
}
