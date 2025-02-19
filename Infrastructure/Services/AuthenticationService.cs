using Application.Interfaces;
using Application.Models.Request;
using Microsoft.Extensions.Options;
using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AutenticacionServiceOptions _options;

        public AuthenticationService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
        {
            _userRepository = userRepository;
            _options = options.Value;
        }

        private User? ValidateUser(AuthenticationRequest authenticationRequest)
        {
            if (string.IsNullOrEmpty(authenticationRequest.UserName) || string.IsNullOrEmpty(authenticationRequest.Password))
                return null;

            var user = _userRepository.GetUserByUserName(authenticationRequest.UserName);

            if (user == null) { return null; }
            else if(user.Email != authenticationRequest.UserName || user.Password != authenticationRequest.Password) 
            { return null; }
            else {  return user; }

        }
          public string Autenticar(AuthenticationRequest authenticationRequest)
          {
           
              //Paso 1: Validamos las credenciales
              var user = ValidateUser(authenticationRequest); //Lo primero que hacemos es llamar a una función que valide los parámetros que enviamos.

              if (user == null)
              {
                  return ("User authentication failed"); //solucionar con exepciones custom
              }


              //Paso 2: Crear el token
              var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey)); //Traemos la SecretKey del Json. agregar antes: using Microsoft.IdentityModel.Tokens;

              var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

              //Los claims son datos en clave->valor que nos permite guardar data del usuario.
              var claimsForToken = new List<Claim>();
              claimsForToken.Add(new Claim("NameIdentifier", user.Id.ToString()));
              claimsForToken.Add(new Claim("Name", user.Name));
              claimsForToken.Add(new Claim("Role", user.Role));


            var jwtSecurityToken = new JwtSecurityToken( //agregar using System.IdentityModel.Tokens.Jwt; Acá es donde se crea el token con toda la data que le pasamos antes.
                _options.Issuer,
                _options.Audience,
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

              var tokenToReturn = new JwtSecurityTokenHandler() //Pasamos el token a string
                  .WriteToken(jwtSecurityToken);

              return tokenToReturn.ToString();
         }

        public class AutenticacionServiceOptions
        {
            public const string AutenticacionService = "AutenticacionService";

            public string Issuer { get; set; }
            public string Audience { get; set; }
            public string SecretForKey { get; set; }
        }
    }
}
