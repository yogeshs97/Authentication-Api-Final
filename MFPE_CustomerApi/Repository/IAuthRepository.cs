using MFPE_CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MFPE_CustomerApi.Repository
{
    public interface IAuthRepository
    {
        public bool UserLoginRepo(Login model);
       
    }
}
