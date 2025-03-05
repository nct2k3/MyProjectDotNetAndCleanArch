using Application.Authentication.Comands.Register;
using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Contracts;
using Mapster;

using RegisterRequest = Microsoft.AspNetCore.Identity.Data.RegisterRequest;

namespace BockingFood.Common.Mapping;

public class AuthenticationmappingConfig:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
    
        config.NewConfig<RegisterRequest,RegisterCommand>();
        //config.NewConfig<LoginRequest,LoginQuery>();
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map( dest =>dest
                ,src => src.User
            );
        
    }
}