using Application.Application.Common;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistance;
using Domain.Entities;
using MediatR;

namespace Application.Application.Queries;

public class LoginQueryHandler:IRequestHandler<LoginQuery,AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUnitOfWork unitOfWork)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<AuthenticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.Repository<User>();
        User data = await repository.SingleOrDefaultAsync(x => x.Email == request.Email && x.Password == request.Password);
        if (data == null)
        {
            throw new Exception(" email or password is incorrect ");
            
        }
        var token = _jwtTokenGenerator.Generator(data);
        return new AuthenticationResult(data, token);
    }
}