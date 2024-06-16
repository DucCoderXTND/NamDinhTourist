using AutoMapper;
using MediatR;
using TND.Application.Owners.Common;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Owners.GetById
{
    public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerResponse>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }
        public async Task<OwnerResponse> Handle(
            GetOwnerByIdQuery request, 
            CancellationToken cancellationToken)
        {
           var owner = await _ownerRepository.
                GetByIdAsync(request.OwnerId, cancellationToken) 
                ?? throw new NotFoundException(OwnerMessages.NotFound);
            return _mapper.Map<OwnerResponse>(owner);
        }
    }
}
