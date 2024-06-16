using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IBookingRepository
    {
        Task<bool> ExistsByRoomIdAsync(Guid roomId, CancellationToken cancellationToken = default);
        Task<bool> ExistsForHotelAndGuestAsync(Guid hotelId, Guid guestId, CancellationToken cancellationToken = default);
        Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken = default);
        Task<bool> ExistsByIdAndGuestIdAsync(Guid id, Guid guestId, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        //Tìm kiếm theo Id và kiểm tra có muốn in ra thông tin hóa đơn không
        Task<Booking?> GetByIdAsync(Guid id, Guid guestId, bool includeInvoice = false,
            CancellationToken cancellationToken = default);
        Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken= default);
        Task<PaginatedList<Booking>> GetAsync(PaginationQuery<Booking> query,
            CancellationToken cancellationToken = default);

        Task<IEnumerable<Booking>> GetRecentBookingsInDifferentHotelsByGuestId(Guid guestId, int count,
            CancellationToken cancellationToken = default);


    }
}
