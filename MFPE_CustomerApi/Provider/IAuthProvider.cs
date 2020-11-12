using MFPE_CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MFPE_CustomerApi.Provider
{
    public interface IAuthProvider
    {
        public JwtSecurityToken UserLoginProvider(Login model);
    }
}
