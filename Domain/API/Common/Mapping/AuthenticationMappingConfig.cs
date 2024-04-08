using Application.Authentication;
using Application.Authentication.Commands.Payment;
using Application.Authentication.Commands.Register;
using Application.Authentication.Commands.User;
using Application.Authentication.Queries.Login;
using Application.Authentication.Queries.Token;
using Application.Authentication.Queries.Users;
using Contracts.Authentication;
using Contracts.Payment;
using Contracts.Products;
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
            config.NewConfig<OrderInfo, PaymentCommand>()
                .Map(order=>order.orderInfo,src=>src);
            config.NewConfig<string, PaymentCommand>()
                .Map(order=>order.orderInfo,src=>src);

            config.NewConfig<AddProductRequest, ProductCommand>()
           .Map(request => request.productRequest, src => src);

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

            config.NewConfig<User, UserModel>();

            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
                .Map(dest => dest.Token, src => src.Token)
                .Map(dest => dest, src => src.User);

            config.NewConfig<Product, AddProductRequest>();

            config.NewConfig<List<Product>, List<ProductModel>>();

        }
    }
}
