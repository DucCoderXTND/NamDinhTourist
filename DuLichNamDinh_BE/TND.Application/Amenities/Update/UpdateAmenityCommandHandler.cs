using AutoMapper;
using MediatR;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Exceptions;
using TND.Domain.Messages;

namespace TND.Application.Amenities.Update
{
    public class UpdateAmenityCommandHandler : IRequestHandler<UpdateAmenityCommand>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAmenityCommandHandler(IAmenityRepository amenityRepository, 
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateAmenityCommand request, 
            CancellationToken cancellationToken = default)
        {
            var amenityEntity = await _amenityRepository
                .GetByIdAsync(request.AmenityId, cancellationToken)
                ?? throw new NotFoundException(AmenityMessages.WithIdNotFound);

            if((!await _amenityRepository.ExistsByNameAsync(request.Name, cancellationToken)))
            {
                throw new AmenityExistsException(AmenityMessages.WithNameExists);
            }

            _mapper.Map(request, amenityEntity);

            await _amenityRepository.UpdateAsync(amenityEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        }
    }
}
