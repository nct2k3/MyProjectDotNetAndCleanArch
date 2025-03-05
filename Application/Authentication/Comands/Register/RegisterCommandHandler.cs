
using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Persistance;
using MediatR;
using Presentation.Entities;

namespace Application.Authentication.Comands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }

    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;


    public async Task<AuthenticationResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var existingUser = await userRepository.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            throw new Exception("Email already exists");
        }
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = request.Password,
            Role = request.Role
        };
        await userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        
        var token = _jwtTokenGenerator.GenerateJwtToken(user);
        return new AuthenticationResult(user, token);
    }
}