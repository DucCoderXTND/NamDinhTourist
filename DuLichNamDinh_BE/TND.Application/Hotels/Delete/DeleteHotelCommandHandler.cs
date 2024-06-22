using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.Delete
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoomClassRepository _roomClassRepository;

        public DeleteHotelCommandHandler(IHotelRepository hotelRepository,
            IUnitOfWork unitOfWork,IRoomClassRepository roomClassRepository)
        {
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
            _roomClassRepository = roomClassRepository;
        }
        public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken = default)
        {
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
                throw new NotFoundException(HotelMessages.NotFound);

            if (await _roomClassRepository.ExistsByHotelIdAsync(request.HotelId))
                throw new DependentsExistException(HotelMessages.DependentsExist);

            await _hotelRepository.DeleteAsync(request.HotelId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
