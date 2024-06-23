using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.RoomClasses.AddImageToGallery
{
    public class AddImageToRoomClassGalleryCommandHandler : IRequestHandler<AddImageToRoomClassGalleryCommand>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddImageToRoomClassGalleryCommandHandler(IRoomClassRepository roomClassRepository,
            IImageRepository imageRepository, IUnitOfWork unitOfWork)
        {
            _roomClassRepository = roomClassRepository;
            _imageRepository = imageRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(AddImageToRoomClassGalleryCommand request, 
            CancellationToken cancellationToken = default)
        {
            if(await _roomClassRepository.ExistsByIdAsync(request.RoomClassId))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            await _imageRepository.CreateAsync(
                request.Image, request.RoomClassId,
                Domain.Enums.ImageType.Gallery,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }
}
