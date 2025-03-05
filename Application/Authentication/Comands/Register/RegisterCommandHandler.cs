
using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistance;
using MediatR;
using Presentation.Entities;

namespace Application.Authentication.Comands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserReopsitory userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserReopsitory _userRepository;

    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
    
        if (_userRepository.GetUserByEmail(request.Email) is not null)
        {
            throw new Exception("User already exists");
        }
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
        _userRepository.AddUser(user);

        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        return new AuthenticationResult(user, token);
    }
}