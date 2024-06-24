using AutoMapper;
using MediatR;
using TND.Application.RoomClasses.GetById;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.Discounts.Get
{
    public class GetDiscountsQueryHandler : IRequestHandler<GetDiscountsQuery, PaginatedList<DiscountResponse>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;

        public GetDiscountsQueryHandler(IRoomClassRepository roomClassRepository, 
            IDiscountRepository discountRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<DiscountResponse>> Handle(GetDiscountsQuery request, CancellationToken cancellationToken = default)
        {
            if(!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            var query = new PaginationQuery<Discount>(
                dc => dc.RoomClassId == request.RoomClassId,
                request.sortOder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize);

            var owners = await _discountRepository.GetAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<DiscountResponse>>(owners);
        }
    }
}
