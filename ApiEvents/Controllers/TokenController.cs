using ApiEvents.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiEvents.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController: ControllerBase
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet("{name}/{permission}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> CreateToken(string name,string permission)
        {
            string token = _tokenService.GenerateTokenEvents(name, permission);
            return Ok(token);
        }
    }
}
