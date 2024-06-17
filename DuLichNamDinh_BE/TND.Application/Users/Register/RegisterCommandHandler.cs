using AutoMapper;
using MediatR;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Users.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRoleRepository _roleRepository;

        public RegisterCommandHandler(IUserRepository userRepository, IMapper mapper,
            IUnitOfWork unitOfWork, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _roleRepository = roleRepository;
        }
        public async Task Handle(RegisterCommand request, CancellationToken cancellationToken = default)
        {
            var role = await _roleRepository.GetByNameAsync(request.Role, cancellationToken)
                ?? throw new InvalidRoleException(UserMessages.InvalidRole);

            if(await _userRepository.ExistsByEmailAsync(request.Email, cancellationToken))
            {
                throw new UserWithEmailAlreadyExistsException(UserMessages.WithEmailExists);
            }

            var userToAdd = _mapper.Map<User>(request);
            userToAdd.Roles.Add(role);

            await _userRepository.CreateAsync(userToAdd, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
