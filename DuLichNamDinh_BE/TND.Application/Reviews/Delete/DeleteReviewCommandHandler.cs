using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Reviews.Delete
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;

        public DeleteReviewCommandHandler(IReviewRepository reviewRepository, IUnitOfWork unitOfWork,
            IHotelRepository hotelRepository, IUserRepository userRepository)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
        }
        public async Task Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
            {
                throw new NotFoundException(HotelMessages.NotFound);
            }

            if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }

            var review = await _reviewRepository
                .GetByIdAsync(request.ReviewId, request.HotelId, request.GuestId, cancellationToken)
                ?? throw new NotFoundException(ReviewMessages.NotFoundForUserForHotel);

            var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId,
                cancellationToken);

            var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId,
                cancellationToken);

            ratingSum -= review.Rating;

            reviewsCount--;

            var newRating = 1.0 * ratingSum / reviewsCount;

            await _hotelRepository.UpdateReviewById(request.HotelId, newRating, cancellationToken);

            await _reviewRepository.DeleteAsync(request.ReviewId, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            

        }
    }
}
