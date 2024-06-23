using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.RoomClasses.Delete
{
    public class DeleteRoomClassCommandHandler : IRequestHandler<DeleteRoomClassCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomRepository _roomRepository;

        public DeleteRoomClassCommandHandler(IRoomClassRepository roomClassRepository, 
            IUnitOfWork unitOfWork, IRoomRepository roomRepository)
        {
            _roomClassRepository = roomClassRepository;
            _unitOfWork = unitOfWork;
            _roomRepository = roomRepository;
        }
        public async Task Handle(DeleteRoomClassCommand request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
                throw new NotFoundException(RoomClassMessages.NotFound);

            if (await _roomRepository.ExistsByRoomClassIdAsync(request.RoomClassId, cancellationToken))
                throw new DependentsExistException(RoomClassMessages.DependentsExist);

            await _roomClassRepository.DeleteAsync(request.RoomClassId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
