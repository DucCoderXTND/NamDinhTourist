using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Application.Owners.Common;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Models;

namespace TND.Application.Owners.Get
{
    public class GetOwnersQueryHandler : IRequestHandler<GetOwnersQuery,
        PaginatedList<OwnerResponse>>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IMapper _mapper;

        public GetOwnersQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
        {
            _ownerRepository = ownerRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<OwnerResponse>> Handle(GetOwnersQuery request,
            CancellationToken cancellationToken)
        {
            var query = new PaginationQuery<Owner>(
                GetSearchExpression(request.SearchTerm),
                request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize);

            var owners = await _ownerRepository.GetAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<OwnerResponse>>(owners);
        }

        private static Expression<Func<Owner, bool>> GetSearchExpression(string? searchTerm)
        {
            return searchTerm is null 
                ? _ => true
                : o => o.FirstName.Contains(searchTerm) || o.LastName.Contains(searchTerm);
        }

    }
}
