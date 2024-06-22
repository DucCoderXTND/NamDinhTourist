using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Models;

namespace TND.Application.Hotels.GetHotelForManagementQuery
{
    public class GetHotelsForManagementQueryHandler : IRequestHandler<GetHotelsForManagementQuery, PaginatedList<HotelForManagementResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelsForManagementQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<HotelForManagementResponse>> Handle(GetHotelsForManagementQuery request, 
            CancellationToken cancellationToken)
        {
            var query = new PaginationQuery<Hotel>(
                GetSearchExpression(request.SearchTerm),
                request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize);

            var owners = await _hotelRepository.GetForManagementAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<HotelForManagementResponse>>(owners);
        }

        private static Expression<Func<Hotel, bool>> GetSearchExpression(string? searchTerm)
        {
            return searchTerm is null 
                ? _ => true
                : hotel => hotel.Name.Contains(searchTerm) || hotel.City.Name.Contains(searchTerm);
        } 
    }
}
