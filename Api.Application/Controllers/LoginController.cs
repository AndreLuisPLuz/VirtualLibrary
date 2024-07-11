using Api.Domain.DataTransfer.Answer;
using Api.Domain.DataTransfer.Payload;
using Api.Domain.DataTransfer.Session;
using Api.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Application.Controllers
{
    [ApiController]
    [Route("/api/v1/login")]
    public class LoginController : ControllerBase
    {
        private AuthSession _authSession;

        public LoginController(AuthSession authSession)
        {
            _authSession = authSession;
        }

        [HttpPost]
        public async Task<ActionResult> TryLogin(
                [FromServices] ILoginService loginService,
                [FromServices] IUserService userService,
                [FromBody] LoginPayload payload)
        {
            var isLoginSuccesful = await loginService.TryLogin(payload);

            if (!isLoginSuccesful) return BadRequest();

            var user = await userService.FetchByUsernameAsync(payload.Username);

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSession.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name)
            };

            var Sectoken = new JwtSecurityToken(
                _authSession.Issuer,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);
            var answer = new LoginAnswer(token);

            return Ok(answer);
        }
    }
}
