using AutoMapper;
using MediatR;
using TND.Application.RoomClasses.GetById;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Interfaces.Services;
using TND.Domain.Messages;

namespace TND.Application.Discounts.Create
{
    public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, DiscountResponse>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IRoomClassRepository roomClassRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider)
        {
            _discountRepository = discountRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<DiscountResponse> Handle(CreateDiscountCommand request, CancellationToken cancellationToken = default)
        {
            if(await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            if(await _discountRepository.ExistsInTimeIntervalAsync(request.StartDateUtc, request.EndDateUtc, cancellationToken))
            {
                throw new DiscountIntervalsConflictException(DiscountMessages.InDateIntervalExists);
            }

            var discount = _mapper.Map<Discount>(request);

            discount.CreatedAtUtc = _dateTimeProvider.GetCurrentDateTimeUtc();

            var createDiscount =  await _discountRepository.CreateAsync(discount, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
            return _mapper.Map<DiscountResponse>(createDiscount);
        }
    }
}
