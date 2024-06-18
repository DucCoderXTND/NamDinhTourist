using MediatR;
using Microsoft.AspNetCore.Http;

namespace TND.Application.Hotels.AddToGallery
{
    public record AddImageToHotelGalleryCommand(Guid HotelId, IFormFile Image) : IRequest;
}
