using Application.Authentication;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.User;
using Application.Authentication.Queries;
using Application.Authentication.Queries.Token;
using Application.User.Queries;
using Contracts.Authentication;
using Contracts.UsersContracts;
using Domain.Entites;
using Mapster;

namespace API.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();
            config.NewConfig<LoginRequest, LoginQuery>();
            config.NewConfig<UserRequest, UserQuery>();
            config.NewConfig<TokenModel, TokenQuery>();

            config.NewConfig<string, UserCommand>()
             .Map(dest => dest.userUpdate, src => src); ;

            config.NewConfig<UserImageCreateRequest, UserCommand>()
                  .Map(dest => dest.userUpdate, src => src); ;

            config.NewConfig<UserCommand, User>()
                .Map(dest=>dest,src=>src.userUpdate);
            config.NewConfig<UpdatePasswordRequest, UserCommand>()
                  .Map(dest => dest.userUpdate, src => src); ;

            config.NewConfig<UserUpdateRequest, UserCommand>()
                 .Map(dest => dest.userUpdate, src => src.userUpdate);

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);


        }
    }
}
