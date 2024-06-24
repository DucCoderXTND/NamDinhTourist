using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Rooms.Update
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRoomCommandHandler(IRoomClassRepository roomClassRepository, IRoomRepository roomRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            if(!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            if(!await _roomRepository.ExistsByNumberInRoomClassAsync(request.Number, request.RoomClassId, cancellationToken))
            {
                throw new RoomWithNumberExistsInRoomClassException(RoomClassMessages.DuplicatedRoomNumber);
            }

            var roomEntity = await _roomRepository.GetByIdAsync(
                                    request.RoomClassId, request.RoomId, cancellationToken);

            if (roomEntity is null)
            {
                throw new NotFoundException(RoomClassMessages.RoomNotFound);
            }

            _mapper.Map(request, roomEntity);
            await _roomRepository.UpdateAsync(roomEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
