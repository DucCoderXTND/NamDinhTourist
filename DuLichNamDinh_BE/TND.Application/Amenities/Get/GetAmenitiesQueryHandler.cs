﻿using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Application.Amenities.Common;
using TND.Domain.Entities;
using TND.Domain.Enums;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Models;

namespace TND.Application.Amenities.Get
{
    public class GetAmenitiesQueryHandler : IRequestHandler<GetAmenitiesQuery, PaginatedList<AmenityResponse>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IMapper _mapper;

        public GetAmenitiesQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
        {
            _amenityRepository = amenityRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<AmenityResponse>> Handle(GetAmenitiesQuery request,
            CancellationToken cancellationToken)
        {
            var query = new PaginationQuery<Amenity>(
                GetSearchExpression(request.SearchTerm),
                request.SortOrder ?? SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize
                );

            var owners = await _amenityRepository.GetAsync(query, cancellationToken);
            return _mapper.Map<PaginatedList<AmenityResponse>>(owners);
        }

        private static Expression<Func<Amenity, bool>> GetSearchExpression(string? searchTerm)
        {
            return string.IsNullOrEmpty(searchTerm) 
                ? _ => true
                : o => o.Name.Contains(searchTerm);
        }
    }
}
