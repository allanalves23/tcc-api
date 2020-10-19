using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using API.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Security
{
    public class AccessManager
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;

        public AccessManager(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<bool> ValidateCredentials(UserModel user)
        {
            bool credenciaisValidas = false;
            if (user != null && !String.IsNullOrWhiteSpace(user.UserID))
            {
                ApplicationUser userIdentity = await _userManager.FindByNameAsync(user.UserID);

                if (userIdentity != null)
                {
                    SignInResult resultadoLogin = await _signInManager.CheckPasswordSignInAsync(
                        userIdentity, 
                        user.Password, 
                        false
                    );

                    if (resultadoLogin.Succeeded)
                        credenciaisValidas = await _userManager.IsInRoleAsync(userIdentity, RolesModel.Product);
                }
            }

            return credenciaisValidas;
        }

        public TokenModel GenerateToken(UserModel user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserID, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserID)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao.AddMinutes(_tokenConfigurations.Minutes);

            var handler = new JwtSecurityTokenHandler();
            
            SecurityToken securityToken = handler.CreateToken(
                new SecurityTokenDescriptor
                    {
                        Issuer = _tokenConfigurations.Issuer,
                        Audience = _tokenConfigurations.Audience,
                        SigningCredentials = _signingConfigurations.SigningCredentials,
                        Subject = identity,
                        NotBefore = dataCriacao,
                        Expires = dataExpiracao
                    });

            return new TokenModel(
                dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                handler.WriteToken(securityToken)
            );
        }
    }
}