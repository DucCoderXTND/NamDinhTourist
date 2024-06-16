using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Owners.Update
{
    public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateOwnerCommandHandler(IOwnerRepository ownerRepository,
            IMapper mapper, IUnitOfWork unitOfWork)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken = default)
        {
            var ownerEntity = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                ?? throw new NotFoundException(OwnerMessages.NotFound);

            _mapper.Map(request, ownerEntity);

            await _ownerRepository.UpdateAsync(ownerEntity, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
