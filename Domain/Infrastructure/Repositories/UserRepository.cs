using Application.Authentication.Commands.User;
using Application.Authentication.Queries.Login;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Contracts.UsersContracts;
using Dapper;
using Domain.Entites;
using MapsterMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IJwtTokenGenerator _tokenGenerator;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly string _userContentFolder;
        private const string USER_CONTENT_FOLDER_NAME = "assets\\img";

        public UserRepository(IWebHostEnvironment webHostEnvironment,UserManager<User> userManager, RoleManager<Roles> roleManager, IJwtTokenGenerator tokenGenerator,ApplicationDbContext context,IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
            _context = context;
            _mapper = mapper;
            _userContentFolder = Path.Combine(webHostEnvironment.WebRootPath, USER_CONTENT_FOLDER_NAME);

        }

        public async Task<List<User>> GetAllUser(string key)
        {
            using (var db = new ApplicationDbContext())
            {
                var sql = $@"SELECT TOP 100 * FROM AspNetUsers";
                var x = db.ExecuteQuery<User>(sql).ToList();
                return x;
            } 
        }

        public async Task<UserModel?> GetUserByEmail(string email)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = await _userManager.FindByNameAsync(email);
                    var userModel = _mapper.Map<UserModel>(user); ;

                    if (userModel == null)
                    {
                        return null;
                    }
                    else
                    {
                        return userModel;
                    }
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserModel> GetUserById(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            var userModel = _mapper.Map<UserModel>(user); ;
            return userModel;
        }

        public async Task<(bool, string, string)> LoginUser(LoginQuery userData)
        {
            var user = await _userManager.FindByNameAsync(userData.Email);
            if (user == null) return (false, "", "");
            var checkValidUser = user.Password.Equals(userData.Password);

            if (!checkValidUser)
                return (false, null, null);
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = _tokenGenerator.GenerateToken( authClaims);
            var refreshToken = _tokenGenerator.GenerateRefreshToken();


            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

            await _userManager.UpdateAsync(user);
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            return (true, token, refreshToken);
        }

        public async Task<(bool, string, string)> RegisterUser(User user)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var userExist = await _userManager.FindByEmailAsync(user.Email);
                    if (userExist != null)
                    {
                        return (false, "", "");
                    }

                    User userIdentity = new()
                    {
                        Email = user.Email,
                        SecurityStamp = user.Id.ToString(),
                        UserName = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Password = user.Password,
                        ImagePath = ""
                    };
                    var result = await _userManager.CreateAsync(userIdentity, user.Password);

                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var token = _tokenGenerator.GenerateToken( authClaims);
                    var refreshToken = _tokenGenerator.GenerateRefreshToken();

                    await _userManager.UpdateAsync(userIdentity);
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);


                    if (!result.Succeeded)
                    {
                        return (false, "", "");
                    }

                    return (true, token, refreshToken);

                }
            }
            catch (Exception ex)
            {
                return (false, "", "");
            }
        }

        public async Task<User> UpdateByEmail(UserCommand updateUser)
        {
            try
            {
                var updateUser2 = (UserUpdateData)updateUser.userUpdate;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == updateUser2.Email);
                if (user == null)
                {
                    return null;
                }
                user.FirstName = updateUser2.FirstName;
                user.LastName = updateUser2.LastName;
                user.PhoneNumber = updateUser2.PhoneNumber;
                user.Country = updateUser2.Country;
                user.City = updateUser2.City;
                user.BirthDay = updateUser2.BirthDay ?? user.BirthDay;
                user.Organization = updateUser2.Organization;
                user.Role = updateUser2.Role;
                user.Address = updateUser2.Address;
                user.ZipCode = updateUser2.ZipCode;
                user.Department = updateUser2.Department;

                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UpdatePassword(UserCommand updateUser)
        {
            try
            {
                var updateUser2 = (UpdatePasswordRequest)updateUser.userUpdate;
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == updateUser2.Email);
                if (user == null)
                {
                    return null;
                }
                if(user.Password!= updateUser2.OldPassword)
                {
                    return null;
                }
                if (!updateUser2.NewPassword.Equals(updateUser2.ConfirmNewPassword))
                {
                    return null;
                }
                user.Password = updateUser2.NewPassword;
                await _context.SaveChangesAsync();
                return user;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> UploadImage(UserCommand request)
        {
            var updateUser2 = (UserImageCreateRequest)request.userUpdate;
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == updateUser2.Email);
            if (user == null)
                return null;
            var imagePath = await SaveFile(updateUser2.ImageFile);
            if (!string.IsNullOrEmpty(imagePath))
            {
                user.ImagePath = imagePath;
                _context.SaveChanges();
                return user;
            }
            return user;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            try
            {
                var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";

                var filePath = Path.Combine(_userContentFolder, fileName);
                using var output = new FileStream(filePath, FileMode.Create);
                await file.OpenReadStream().CopyToAsync(output);
                return fileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}