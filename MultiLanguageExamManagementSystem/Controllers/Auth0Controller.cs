using Microsoft.AspNetCore.Mvc;
using MultiLanguageExamManagementSystem.Helpers.Models;
using MultiLanguageExamManagementSystem.Services.IServices;

namespace MultiLanguageExamManagementSystem.Controllers
{
    [ApiController]
    [Route("api/auth0")]
    public class Auth0Controller : ControllerBase
    {
        private readonly IAuth0Service _auth0Service;

        public Auth0Controller(IAuth0Service auth0Service)
        {
            _auth0Service = auth0Service;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] Auth0SignupRequest signupRequest)
        {
            try
            {
                var result = await _auth0Service.SignupUserAsync(signupRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken([FromBody] Auth0TokenRequest tokenRequest)
        {
            try
            {
                var result = await _auth0Service.GetTokenAsync(tokenRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}