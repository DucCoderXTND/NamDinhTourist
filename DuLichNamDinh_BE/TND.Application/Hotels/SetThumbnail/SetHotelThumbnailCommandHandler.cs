using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.SetThumbnail
{
    public class SetHotelThumbnailCommandHandler : IRequestHandler<SetHotelThumbnailCommand>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetHotelThumbnailCommandHandler(IHotelRepository hotelRepository,
            IImageRepository imageRepository,
            IUnitOfWork unitOfWork)
        {
            _hotelRepository = hotelRepository;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(SetHotelThumbnailCommand request,
            CancellationToken cancellationToken = default)
        {
            if(!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
            {
                throw new NotFoundException(HotelMessages.NotFound);
            }

            await _imageRepository.DeleteForAsync(
                request.HotelId,
                Domain.Enums.ImageType.Thumbnail,
                cancellationToken);

             await _imageRepository.CreateAsync(
                request.Image,
                request.HotelId,
                Domain.Enums.ImageType.Thumbnail,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
