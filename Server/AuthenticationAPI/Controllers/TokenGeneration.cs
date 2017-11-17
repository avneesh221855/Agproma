using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
    [Route("api/[controller]")]
   // [Produces("application/json")]
    public class TokenGeneration :Controller
    {
        //some config in the appsettings.json
        private readonly IOptions<Audience> _settings;
        private readonly IConfiguration _config;
        public TokenGeneration(IConfiguration config, IOptions<Audience> settings)
        {
            _config = config;
            _settings = settings;
        }
        //this method will be called by the angular app with the user object for getting the JWT token
        [HttpPost]
        [Route("createtoken")]
        public string TokenGenerationAction([FromBody]User user)
        {
            //creating the user object for the given  user from the frond end app
            User tokenUser = new User { UserName = user.UserName, Email = user.Email };
            //calling the function for the JWT token for respecting user
            string value = GetJWT(tokenUser);
            //returning the token to the angular app
            return value;
        }

        private string GetJWT(User tokenUser)
        {
            //setting the claims for the user credential name and email
            var claims = new[]
           {
                
                new Claim(JwtRegisteredClaimNames.GivenName, tokenUser.UserName),
                new Claim(JwtRegisteredClaimNames.Email, tokenUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            //get the secret key value form the app setting config for encoding the claim 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //defining the JWT token essential information and setting its expiration time
            var token = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            //defing the response of the token 
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
            //convert into the json by serialing the response object
            return JsonConvert.SerializeObject(response);
        }


        [HttpPost]
        [Route("createtokenforfbandgoogle/{user}")]
        public string TokenGenerationActionForFBandGoogle(string user)
        {
            //creating the user object for the given  user from the frond end app
            SocialSignup tokenUser = new SocialSignup {  Email = user };
            //calling the function for the JWT token for respecting user
            string value = GetJWTForFBandGoogle(tokenUser);
            //returning the token to the angular app
            return value;
        }

        private string GetJWTForFBandGoogle(SocialSignup tokenUser)
        {
            //setting the claims for the user credential name and email
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Email, tokenUser.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            //get the secret key value form the app setting config for encoding the claim 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Value.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //defining the JWT token essential information and setting its expiration time
            var token = new JwtSecurityToken(
                issuer: _settings.Value.Iss,
                audience: _settings.Value.Aud,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );
            //defing the response of the token 
            var response = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
            //convert into the json by serialing the response object
            return JsonConvert.SerializeObject(response);
        }
    }
}
