using AutoMapper;
using MediatR;
using TND.Application.Amenities.Common;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Amenities.Create
{
    public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, AmenityResponse>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAmenityCommandHandler(IAmenityRepository amenityRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<AmenityResponse> Handle(CreateAmenityCommand request,
            CancellationToken cancellationToken = default)
        {
            if(await _amenityRepository.ExistsByNameAsync(request.Name, cancellationToken))
            {
                throw new AmenityExistsException(AmenityMessages.WithNameExists);
            }

            var createdAmenity = await _amenityRepository.CreateAsync(
                _mapper.Map<Amenity>(request),
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<AmenityResponse>(createdAmenity);
        }
    }
}
