using MediatR;
using Microsoft.AspNetCore.Http;

namespace TND.Application.Hotels.SetThumbnail
{
     public record SetHotelThumbnailCommand(
         Guid HotelId,
         IFormFile Image) : IRequest;
}
