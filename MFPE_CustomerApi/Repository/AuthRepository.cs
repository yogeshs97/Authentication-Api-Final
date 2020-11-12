using MFPE_CustomerApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MFPE_CustomerApi.Repository
{
	public class AuthRepository : IAuthRepository
	{
		private readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthRepository));
		private static List<Login> GetLogins = new List<Login>()
		{
			new Login(){ Username="Sakil",Password="Sakil@55",IsEmployee=true},
			new Login(){ Username="Rohit",Password="Rohit@55",IsEmployee=false},
			new Login(){ Username="Yogesh",Password="Yogesh@55",IsEmployee=true},
			new Login(){ Username="Kanika",Password="Kanika@55",IsEmployee=false},
		};
		public bool UserLoginRepo(Login model)
		{
			bool authorized = false;
			try
			{
				foreach (Login users in GetLogins.ToList())
				{
					if (users.Username == model.Username && users.Password == model.Password && users.IsEmployee == model.IsEmployee)
						authorized = true;
				}
			}
			catch (Exception e)
			{
				_logger.Error("Exception Occured while authenticating users" + e.Message);
			}
			return authorized;
		}
	}
}
