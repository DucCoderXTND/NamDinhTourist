using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Rooms.Create
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, Guid>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRoomCommandHandler(IRoomClassRepository roomClassRepository,
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
                throw new NotFoundException(RoomClassMessages.NotFound);

            if (await _roomRepository.ExistsByNumberInRoomClassAsync(request.Number, request.RoomClassId, cancellationToken))
            {
                throw new RoomWithNumberExistsInRoomClassException(RoomClassMessages.DuplicatedRoomNumber);
            }

            var createRoom = await _roomRepository.CrateAsync(
                                _mapper.Map<Room>(request),
                                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return createRoom.Id;
        }
    }
}
