using AutoMapper;
using MediatR;
using TND.Application.Reviews.Common;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.Reviews.GetByHotelId
{
    public class GetReviewsByHotelIdQueryHandler : IRequestHandler<GetReviewsByHotelIdQuery, PaginatedList<ReviewResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public GetReviewsByHotelIdQueryHandler(IHotelRepository hotelRepository, IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<ReviewResponse>> Handle(GetReviewsByHotelIdQuery request,
            CancellationToken cancellationToken)
        {
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
            {
                throw new NotFoundException(HotelMessages.NotFound);
            }

            var query = new PaginationQuery<Review>(
                r => r.HotelId == request.HotelId,
                request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize);

            var owners = await _reviewRepository.GetAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<ReviewResponse>>(owners);
        }
    }
}
