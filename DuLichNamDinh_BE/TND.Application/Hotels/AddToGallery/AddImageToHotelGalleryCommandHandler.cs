using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.AddToGallery
{
    public class AddImageToHotelGalleryCommandHandler : IRequestHandler<AddImageToHotelGalleryCommand>
    {
        private readonly IImageRepository _imageRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddImageToHotelGalleryCommandHandler(IImageRepository imageRepository, 
            IHotelRepository hotelRepository, IUnitOfWork unitOfWork
            )
        {
            _imageRepository = imageRepository;
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddImageToHotelGalleryCommand request, 
            CancellationToken cancellationToken = default)
        {
            if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
                throw new NotFoundException(HotelMessages.NotFound);

            await _imageRepository
                .CreateAsync(
                request.Image, 
                request.HotelId,
                Domain.Enums.ImageType.Gallery,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);


        }
    }
}
