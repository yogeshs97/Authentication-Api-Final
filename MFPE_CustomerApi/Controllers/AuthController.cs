using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Transactions;
using MFPE_CustomerApi.Models;
using MFPE_CustomerApi.Provider;
using MFPE_CustomerApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MFPE_CustomerApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthProvider _authProvider;
        private log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(AuthController));

        public AuthController(IAuthProvider authProvider)
        {
            this._authProvider = authProvider;
        }
        [HttpPost]
        [Route("UserLogin")]
        public IActionResult UserLogin([FromBody] Login model)
        {
            _logger.Info(model.Username + " requested to login application");
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            try
            {
                var token = _authProvider.UserLoginProvider(model);
                if (token == null)
                {
                    _logger.Warn(model.Username + "Failed to login");
                    return new StatusCodeResult(500);
                }
                else
                {
                    _logger.Info(model.Username + " logged in successfully");
                    return Ok(token);
                }
            }
            catch (Exception ex)
            {
                _logger.Warn("Exception occured in AuthController while calling authProvider as :" + ex.Message);
                return new StatusCodeResult(500);
            }
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            return Content("Hii there");
        }
    }
}
