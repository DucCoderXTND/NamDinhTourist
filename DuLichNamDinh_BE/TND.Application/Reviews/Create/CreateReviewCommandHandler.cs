using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TND.Application.Reviews.Common;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Reviews.Create
{
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ReviewResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHotelRepository _hotelRepository;
        private readonly IBookingRepository _bookingRepository;

        public CreateReviewCommandHandler(IUserRepository userRepository,
            IReviewRepository reviewRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IHotelRepository hotelRepository, IBookingRepository bookingRepository)
        {
            _userRepository = userRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hotelRepository = hotelRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<ReviewResponse> Handle(CreateReviewCommand request,
            CancellationToken cancellationToken = default)
        {
            if(!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
            {
                throw new NotFoundException(HotelMessages.NotFound);
            }
            if(!await _bookingRepository.ExistsForHotelAndGuestAsync(request.HotelId, request.GuestId, cancellationToken))
            {
                throw new GuestDidNotBookHotelException(BookingMessages.NoBookingForGuestInHotel);
            }
            if (await _reviewRepository.ExistsByGuestAndHotelIds(request.GuestId, request.HotelId, cancellationToken))
            {
                throw new ReviewAlreadyExistsException(ReviewMessages.GuestAlreadyReviewedHotel);
            }

            var ratingSum = await _reviewRepository.GetTotalRatingForHotelAsync(request.HotelId, cancellationToken);

            var reviewsCount = await _reviewRepository.GetReviewCountForHotelAsync(request.HotelId, cancellationToken);

            ratingSum += request.Rating;

            reviewsCount++;

            //1.0 * ratingSum trong đoạn mã có mục đích đảm bảo rằng phép chia sau đó
            //sẽ thực hiện dưới dạng số thực, thay vì chia nguyên 
            var newRating = 1.0 * ratingSum / reviewsCount;

            await _hotelRepository.UpdateReviewById(request.HotelId, newRating, cancellationToken);

            var createdReview = await _reviewRepository.CreateAsync(_mapper.Map<Review>(request), cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReviewResponse>(createdReview);
        }
    }
}
