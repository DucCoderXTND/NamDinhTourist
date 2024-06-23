using MediatR;
using Microsoft.AspNetCore.Http;

namespace TND.Application.RoomClasses.AddImageToGallery
{
    public record AddImageToRoomClassGalleryCommand(
        Guid RoomClassId,
        IFormFile Image) : IRequest;
}
