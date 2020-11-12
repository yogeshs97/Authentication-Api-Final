using MFPE_CustomerApi.Models;
using MFPE_CustomerApi.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MFPE_CustomerApi.Provider
{
	public class AuthProvider : IAuthProvider
	{
		private readonly IAuthRepository _authRepository;
		private readonly ITokenProvider _tokenProvider;
		private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthProvider));

		public AuthProvider(IAuthRepository authRepository, ITokenProvider tokenProvider)
		{
			this._authRepository = authRepository;
			this._tokenProvider = tokenProvider;
		}
		public JwtSecurityToken UserLoginProvider(Login model)
		{
			JwtSecurityToken token = null;
			try
			{
				bool authorizeSucceed = _authRepository.UserLoginRepo(model);
				if (authorizeSucceed)
				{
					_logger.Info(model.Username + " Authenticated successfully");
					token = _tokenProvider.GenerateJWTToken(model);
				}
				else
				{
					_logger.Warn(model.Username + " Attempted to login with invalid credentials");
				}
			}
			catch (Exception ex)
			{
				_logger.Warn("Some exception occured while validating user/generation JWT token as follow " + ex.Message);
			}
			return token;
		}
	}
}
