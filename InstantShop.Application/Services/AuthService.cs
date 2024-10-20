using InstantShop.Application.AuthClass;
using InstantShop.Application.Responses;
using InstantShop.Application.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InstantShop.Application.Services
{
    public class AuthService : IAuthService
    {

        private ApplicationUser ApplicationUser;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = configuration;
            ApplicationUser = new();
        }


        public async Task<IEnumerable<IdentityError>> Reister(Register register)
        {
            ApplicationUser.FirstName = register.FirstName;
            ApplicationUser.LastName = register.LastName;
            ApplicationUser.Email = register.Email;
            ApplicationUser.UserName = register.Email;
            var result = await _userManager.CreateAsync(ApplicationUser, register.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApplicationUser, "ADMIN");
            }
           
            return result.Errors;
        }
        public async Task<object> Login(Login login)
        {
            ApplicationUser = await _userManager.FindByEmailAsync(login.Email);
            if (ApplicationUser == null)
            {
                return "Email Not Found";
            }
            var passcheck = await _userManager.CheckPasswordAsync(ApplicationUser, login.Password);
            var result = await _signInManager
                .PasswordSignInAsync(ApplicationUser, login.Password, isPersistent: true, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                var token = await GenerateToken();
                LoginResponce loginResponce = new LoginResponce
                {
                    UserId = ApplicationUser.Id,
                    Token = token
                };
                return loginResponce;
            }
            else
            {
                if (result.IsNotAllowed)
                {
                    return "invalid Email";
                }
                if (result.IsLockedOut)
                {
                    return "Locked Email";
                }
                if (passcheck == false)
                {
                    return "Invalid password";
                }
                else
                {
                    return "Login Failed";
                }
            }
        }

        public async Task<string> GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWTSettings:Key"]));
            var signincreadential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var roles = await _userManager.GetRolesAsync(ApplicationUser);
            var claimrole = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
            List<Claim> claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email,ApplicationUser.Email)
            }.Union(claimrole).ToList();
            var token = new JwtSecurityToken(
                issuer: _config["JWTSettings:Issuer"],
                audience: _config["JWTSettings:Audience"],
                claims: claims,
                signingCredentials: signincreadential,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_config["JWTSettings:DurationInMunutes"]))
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
