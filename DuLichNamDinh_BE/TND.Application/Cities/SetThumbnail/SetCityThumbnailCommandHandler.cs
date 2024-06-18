using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Cities.SetThumbnail
{
    public class SetCityThumbnailCommandHandler : IRequestHandler<SetCityThumbnailCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageRepository _imageRepository;

        public SetCityThumbnailCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork,
            IImageRepository imageRepository)
        {
            _cityRepository = cityRepository;
            _unitOfWork = unitOfWork;
            _imageRepository = imageRepository;
        }
        public async Task Handle(SetCityThumbnailCommand request, 
            CancellationToken cancellationToken = default)
        {
            if(!await _cityRepository.ExistsByIdAsync(request.CityId, cancellationToken))
            {
                throw new NotFoundException(CityMessages.NotFound);
            }

            await _imageRepository.DeleteForAsync(
                request.CityId,
                Domain.Enums.ImageType.Thumbnail,
                cancellationToken);

            await _imageRepository.CreateAsync(
                request.Image,
                request.CityId,
                Domain.Enums.ImageType.Thumbnail,
                cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
