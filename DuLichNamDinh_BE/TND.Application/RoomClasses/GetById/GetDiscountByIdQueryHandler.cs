using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.RoomClasses.GetById
{
    public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountResponse>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _discountRepository;

        public GetDiscountByIdQueryHandler(IRoomClassRepository roomClassRepository,
            IMapper mapper, IDiscountRepository discountRepository)
        {
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
            _discountRepository = discountRepository;
        }
        public async Task<DiscountResponse> Handle(GetDiscountByIdQuery request, CancellationToken cancellationToken)
        {
            if(!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            var discount = await _discountRepository.GetByIdAsync(
                request.RoomClassId,
                request.DiscountId,
                cancellationToken
                );

            if(discount is null)
            {
                throw new NotFoundException(DiscountMessages.NotFoundInRoomClass);
            }

            return _mapper.Map<DiscountResponse>(discount);
        }
    }
}
