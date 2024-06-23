using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Discounts.Delete
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository, 
            IRoomClassRepository roomClassRepository,
            IUnitOfWork unitOfWork)
        {
            _discountRepository = discountRepository;
            _roomClassRepository = roomClassRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
                throw new NotFoundException(RoomClassMessages.NotFound);

            if(!await _discountRepository.ExistsByIdAsync(request.RoomClassId, request.DiscountId, cancellationToken))
                throw new NotFoundException(DiscountMessages.NotFoundInRoomClass);

            await _discountRepository.DeleteAsync(request.DiscountId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
