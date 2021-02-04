using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace COMP2001_Authentication_Application.Managers
{
    public interface IJWTManager
    {
        JwtAuthResult GenerateTokens(Claim[] claims, DateTime now);
        // ... other JWT contract requirements for future works, decoding etc...
    }

    public class JWTManager : IJWTManager
    {
        private readonly TokenConfig jwtTokenConfig;
        private readonly byte[] secret;

        public JWTManager(TokenConfig jwtTokenConfig)
        {
            this.jwtTokenConfig = jwtTokenConfig;
            this.secret = Encoding.ASCII.GetBytes(jwtTokenConfig.Secret);
        }

        /// <summary>
        /// Generate a JSON web token, based on the username and the claims and expiration date provided. No refresh tokens will be used.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="now"></param>
        /// <returns></returns>
        public JwtAuthResult GenerateTokens(Claim[] claims, DateTime now)
        {
            var shouldAddAudienceClaim = string.IsNullOrWhiteSpace(claims?.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);
            var jwtToken = new JwtSecurityToken(
                jwtTokenConfig.Issuer,
                shouldAddAudienceClaim ? jwtTokenConfig.Audience : string.Empty,
                claims,
                expires: now.AddMinutes(jwtTokenConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
            };
        }
    }

    public class JwtAuthResult
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
    }
}
