using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.Identity;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Security
{
    public class AccessManager
    {
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurationsModel _tokenConfigurations;

        public AccessManager(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurationsModel tokenConfigurations)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
        }

        public async Task<CredentialModel> ValidateCredentials(UserModel user)
        {
            bool credenciaisValidas = false;
            Usuario userIdentity = null;

            user.Validate();

            if (user != null)
            {
                userIdentity = await _userManager.FindByEmailAsync(user.UserID);

                if (userIdentity != null)
                {
                    SignInResult resultadoLogin = await _signInManager.CheckPasswordSignInAsync(
                        userIdentity, 
                        user.Password, 
                        false
                    );

                    if (resultadoLogin.Succeeded)
                        credenciaisValidas = await _userManager.IsInRoleAsync(userIdentity, RolesModel.Product);
                } else
                    throw new UnauthorizedAccessException("E-mail ou senha inválidos");
            } else
                throw new InvalidOperationException("Ocorreu um erro desconhecido, se persistir reporte");

            return new CredentialModel(credenciaisValidas, new UserModel(userIdentity.Email, userIdentity.UserName));
        }

        public TokenModel GenerateToken(UserModel user)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                new List<Claim> {
                    new Claim("userID", user.UserID),
                    new Claim("userName", user.UserName)
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
                handler.WriteToken(securityToken),
                user
            );
        }
    }
}