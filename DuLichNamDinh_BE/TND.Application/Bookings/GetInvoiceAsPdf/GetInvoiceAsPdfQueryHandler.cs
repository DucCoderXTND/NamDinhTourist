using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Interfaces.Services;
using TND.Domain.Messages;

namespace TND.Application.Bookings.GetInvoiceAsPdf
{
    public class GetInvoiceAsPdfQueryHandler : IRequestHandler<GetInvoiceAsPdfQuery, byte[]>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPdfService _pdfService;

        public GetInvoiceAsPdfQueryHandler(IBookingRepository bookingRepository, IUserRepository userRepository,
            IPdfService pdfService)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _pdfService = pdfService;
        }
        public async Task<byte[]> Handle(GetInvoiceAsPdfQuery request, CancellationToken cancellationToken = default)
        {
            if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }

            var booking = await _bookingRepository
                .GetByIdAsync(request.BookingId, request.GuestId, true, cancellationToken)
                ?? throw new NotFoundException(BookingMessages.NotFoundForGuest);


            return await _pdfService.GeneratePdfFromHtmlAsync(InvoiceDetailsGenerator.GetInvoiceHtml(booking), cancellationToken);
        }
    }
}
