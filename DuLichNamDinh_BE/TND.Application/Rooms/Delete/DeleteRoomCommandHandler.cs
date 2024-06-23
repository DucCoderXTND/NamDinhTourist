using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Rooms.Delete
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBookingRepository _bookingRepository;

        public DeleteRoomCommandHandler(IRoomClassRepository roomClassRepository,
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork,
            IBookingRepository bookingRepository)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _bookingRepository = bookingRepository;
        }
        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsByIdAsync(request.RoomId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            if (!await _roomRepository.ExistsByIdAndRoomClassIdAsync(request.RoomClassId, request.RoomId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.RoomNotFound);
            }

            if(await _bookingRepository.ExistsByRoomIdAsync(request.RoomId))
            {
                throw new DependentsExistException(RoomMessages.DependentsExist);
            }

            await _roomRepository.DeleteAsync(request.RoomId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
