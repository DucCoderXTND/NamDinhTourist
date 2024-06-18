using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Cities.Delete
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
    {
        private readonly ICityRepository _cityRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCityCommandHandler(ICityRepository cityRepository,
            IHotelRepository hotelRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _hotelRepository = hotelRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            if(await _cityRepository.ExistsByIdAsync(request.CityId, cancellationToken))
            {
                throw new NotFoundException(CityMessages.NotFound);
            }
            if(await _hotelRepository.ExistsByCityIdAsync(request.CityId, cancellationToken))
            {
                throw new DependentsExistException(CityMessages.DependentsExist);
            }

            await _cityRepository.DeleteAsync(request.CityId, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
