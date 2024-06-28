using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Bookings.Delete
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public DeleteBookingCommandHandler(IBookingRepository bookingRepository, IUnitOfWork unitOfWork,
            IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteBookingCommand request, CancellationToken cancellationToken = default)
        {
            if(!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }

            if(!await _bookingRepository.ExistsByIdAndGuestIdAsync(request.BookingId, request.GuestId, cancellationToken))
            {
                throw new NotFoundException(BookingMessages.NotFoundForGuest);
            }

            var booking = await _bookingRepository.GetByIdAsync(request.BookingId, cancellationToken);

            if(booking!.CheckInDateUtc <= DateOnly.FromDateTime(DateTime.UtcNow))
            {
                throw new CannotCancelBookingException(BookingMessages.CheckedIn);
            }

            await _bookingRepository.DeleteAsync(request.BookingId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
