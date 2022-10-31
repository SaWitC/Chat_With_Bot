using BotServer.Application.Repositories;
using BotServer.Data.Attributes;
using BotServer.Data.Data;
using BotServer.Domain.ConfigModels;
using BotServer.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BotServer.Data.Repositories
{
    [Service]
    public class AccountRepository : IAccountRepository
    {
        private readonly IOptions<AuthOptions> _options;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        public AccountRepository(IOptions<AuthOptions> options, UserManager<User> userManager, AppDbContext appDbContext)
        {
            _options = options;
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public async Task<string> GenerateJwtToken(User user)
        {
            var authParams = _options.Value;
            var securityKey = authParams.GetSymetricSecurityKey();
            var creditails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName)
            };
            foreach (var VARIABLE in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", VARIABLE));
            }
            foreach(var x in authParams.AllAudience)
            {
                claims.Add(new Claim("aud", x));
            }

            var token = new JwtSecurityToken(
                authParams.Issuer,
                claims:claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: creditails
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
