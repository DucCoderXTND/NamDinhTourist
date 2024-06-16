using AutoMapper;
using MediatR;
using TND.Application.Amenities.Common;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Exceptions;
using TND.Domain.Messages;
namespace TND.Application.Amenities.GetById
{
    public class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, AmenityResponse>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetAmenityByIdQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }
        public async Task<AmenityResponse> Handle(GetAmenityByIdQuery request, CancellationToken cancellationToken)
        {
            var amenity = await _amenityRepository.GetByIdAsync(
                request.AmenityId, cancellationToken) ??
                throw new NotFoundException(AmenityMessages.WithIdNotFound);

            return _mapper.Map<AmenityResponse>(amenity);
        }
    }
}
