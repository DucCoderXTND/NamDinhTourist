using AutoMapper;
using MediatR;
using TND.Application.Owners.Common;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;

namespace TND.Application.Owners.Create
{
    public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerResponse>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateOwnerCommandHandler(IOwnerRepository ownerRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OwnerResponse> Handle(
            CreateOwnerCommand request, 
            CancellationToken cancellationToken)
        {
            var createdOwner = await _ownerRepository.CreateAsync(
                _mapper.Map<Owner>(request),
                cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return _mapper.Map<OwnerResponse>(createdOwner);
        }
    }
}
