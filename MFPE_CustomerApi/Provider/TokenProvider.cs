﻿using MFPE_CustomerApi.Models;
using MFPE_CustomerApi.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MFPE_CustomerApi.Provider
{
    public class TokenProvider : ITokenProvider
    {
        private IConfiguration _config;
        private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(TokenProvider));
        public TokenProvider(IConfiguration config)
        {
            this._config = config;
        }
        public JwtSecurityToken GenerateJWTToken(Login model)
        {
            JwtSecurityToken token = null;
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>();
                if (model.IsEmployee)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Employee"));
                    claims.Add(new Claim("Username", model.Username));
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Customer"));
                    claims.Add(new Claim("Username", model.Username));
                }
                token = new JwtSecurityToken(
                            issuer: _config["Jwt:Issuer"],
                            audience: _config["Jwt:Issuer"],
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: credentials);
            }
            catch (Exception e)
            {
                _logger.Error("Exception occured while generating tokens as " + e.Message);
            }
            return token;
        }

        
    }
}
