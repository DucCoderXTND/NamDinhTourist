using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Auth;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Users.Login
{
    public class RegisterCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public RegisterCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }
        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository
                .AuthenticateAsync(request.email, request.password, cancellationToken)
                ?? throw new CredentialsNotValidException(UserMessages.CredentialsNotValid);

            return _mapper.Map<LoginResponse>(_jwtTokenGenerator.Generate(user));

        }
    }
}
