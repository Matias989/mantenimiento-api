
using AutoMapper;
using mantenimiento_api.Controllers;
using mantenimiento_api.Controllers.RR;
using mantenimiento_api.Controllers.RR.VM;
using mantenimiento_api.Controllers.VM;
using mantenimiento_api.Services.Interfaces;
using mantenimiento_api.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace mantenimiento_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        readonly ILogger _logger;
        readonly IAuthServices _authServices;
        readonly IUsersServices _userServices;

        public AuthController(ILogger<AuthController> logger, IAuthServices authServices,IUsersServices userServices)
        {
            _logger = logger;
            _authServices = authServices;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(AuthReq req)
        {
            _logger.LogInformation("------Start AuthController - Authenticate ------");
            ApiResponseBase<AuthVM> resp = new ApiResponseBase<AuthVM>();
            resp.Data = new AuthVM();
            resp.Successful();
            try
            {

                //Valid User
                _logger.LogInformation("Search User in db");
                
                var user = _userServices.GetUser(req.Email);

                if (user is null)
                {
                    _logger.LogWarning("User not found");
                    return NotFound("User not found");
                }

                //Valid User Password
                _logger.LogInformation("Compare Hashes");
                if (SecurityHelper.CheckHashes(req.Password, user.Password, user.Salt))
                {
                    _logger.LogWarning("Invalid Credentials");
                    return Forbid();
                }

                resp.Data.Token = _authServices.CreateToken(user.Id);

                if (string.IsNullOrEmpty(resp.Data.Token))
                {
                    _logger.LogWarning("Error with token creation");
                    return Problem("Error with token creation", null, null, "Auth Fail", null);
                }

                _logger.LogInformation("Authenticate Succefull");
                return Ok(resp);

            }
            catch (Exception e)
            {
                _logger.LogError("Auth Fail: {0}", e.Message);
                return Problem(e.Message, null, null, "Auth Fail", null);
            }
            finally
            {
                _logger.LogInformation("------Finish AuthController - Authenticate ------");
            }
        }

        [HttpPost]
        [Route("Test")]
        public object testingHash(string password)
        {
            byte[] salt;
            string pass;
            _authServices.hashPassTest(password,out salt, out pass);

            return new { pass, salt};
        }
    }
}