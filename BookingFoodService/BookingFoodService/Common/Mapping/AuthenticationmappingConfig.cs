using Application.Application.Commands.Register;
using Application.Application.Common;
using Application.Application.Queries;
using Contract;
using Contracts;
using Mapster;

namespace BookingFoodService.Common.Mapping;

public class AuthenticationmappingConfig:IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequests, RegisterCommands>();
        config.NewConfig<LoginRequests,LoginQuery>();
        
        config.NewConfig<AuthenticationResult,AuthenticationResponse>()
            .Map(d=>d.Token,src =>src.Token)
            .Map(d => d, src => src.User);
    }
}