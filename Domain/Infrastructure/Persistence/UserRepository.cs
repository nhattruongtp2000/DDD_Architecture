using Application.Authentication.Queries;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Dapper;
using Domain.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Persistence;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Roles> _roleManager;
        private readonly IJwtTokenGenerator _tokenGenerator;

        public UserRepository(UserManager<User> userManager, RoleManager<Roles> roleManager, IJwtTokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenGenerator = tokenGenerator;
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

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                using (var db = new ApplicationDbContext())
                {
                    var user = await _userManager.FindByNameAsync(email);

                    if (user == null)
                    {
                        return null;
                    }
                    else
                    {
                        return user;
                    }
                    //else //remove existing order
                    //{
                    //    using (var trans = db.Database.BeginTransaction())
                    //    {
                    //        try
                    //        {
                    //            db.oRD_HEADERs.RemoveRange(ords);
                    //            db.SaveChanges();
                    //            trans.Commit();
                    //            return true;
                    //        }
                    //        catch (DbEntityValidationException e)
                    //        {
                    //            foreach (var eve in e.EntityValidationErrors)
                    //            {
                    //                foreach (var ve in eve.ValidationErrors)
                    //                {
                    //                }
                    //            }

                    //            trans.Rollback();
                    //            return false;
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            trans.Rollback();
                    //            return false;
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string, string)> LoginUser(LoginQuery userData)
        {
            var user = await _userManager.FindByNameAsync(userData.Email);
            if (user == null) return (false, "", "");
            var checkValidUser = await _userManager.CheckPasswordAsync(user, userData.Password);

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
                        Password = user.Password
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


    }
}
