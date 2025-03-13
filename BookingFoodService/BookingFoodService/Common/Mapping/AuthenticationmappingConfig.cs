using Application.Application.Commands.Oder;
using Application.Application.Commands.Register;
using Application.Application.Common;
using Application.Application.Queries;
using Contract;
using Contracts;
using Mapster;

namespace BookingFoodService.Common.Mapping;

public class AuthenticationmappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequests, RegisterCommands>();
        config.NewConfig<LoginRequests, LoginQuery>();
        config.NewConfig<OrderRequest, OrderCommand>()
            .Map(dest => dest.Details, src => src.Details);

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}