﻿using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Persistence.Repositories
{
    public interface IReviewRepository
    {
        public Task<PaginatedList<Review>> GetAsync(PaginationQuery<Review> query,
            CancellationToken cancellationToken = default);
        Task<Review?> GetByIdAsync(Guid hotelId, Guid reviewId, CancellationToken cancellationToken = default);
        Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default);
        Task<Review?> GetByIdAsync(Guid reviewId, Guid hotelId, Guid guestId, 
            CancellationToken cancellationToken= default);
        Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByGuestAndHotelIds(Guid guestId, Guid hotelId, 
            CancellationToken cancellationToken = default);
        Task<int> GetTotalRatingForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
        Task<int> GetReviewCountForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
        Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
    }
}
