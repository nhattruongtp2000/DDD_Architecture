using Application.Authentication.Commands.Register;
using Application.Common.Interfaces.Persistence;
using ErrorOr;
using MapsterMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.Errors;

namespace Application.Authentication.Commands.User
{
    public class UserCommandHandler : IRequestHandler<UserCommand, ErrorOr<DataResult>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<DataResult>> Handle(UserCommand request, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            var xxx = request.userUpdate.GetType();
            var user = new Domain.Entites.User(); 
            switch (xxx.Name)
            {
                case "UserUpdateData":
                     user = await _userRepository.UpdateByEmail(request);
                    break;
                case "UpdatePasswordRequest":
                     user = await _userRepository.UpdatePassword(request);
                    break;
                case "UserImageCreateRequest":
                    var data = await _userRepository.UploadImage(request);
                    return new DataResult(
           data);
                    break;


            }
            if (user == null)
            {
                return Errors.User.DuplicateEmail;
            }
            return new DataResult(
             user);
            return Errors.User.DuplicateEmail;
        }
    }
}
