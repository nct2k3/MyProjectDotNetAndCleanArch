using Application.Application.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using MediatR;

namespace Application.Application.Commands.Register;

public class RegisterCommandHandler:IRequestHandler<RegisterCommands,AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork  _unitOfWork;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthenticationResult> Handle(RegisterCommands request, CancellationToken cancellationToken)
    {
        var userRepository = _unitOfWork.Repository<User>();
        var checkEmail = await userRepository.SingleOrDefaultAsync(u => u.Email == request.Email);
        if (checkEmail != null)
        {
            throw new Exception("Email already exists");
            
        }

        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Address = request.Address,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password,
            Role = request.Role,
        };
        await userRepository.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();
        var toke = _jwtTokenGenerator.Generator(user);
        return new AuthenticationResult(user, toke);
    }
}