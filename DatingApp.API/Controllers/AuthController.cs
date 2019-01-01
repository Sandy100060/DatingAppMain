using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthRepository _repo { get; set; }
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            userForRegisterDto.Username = userForRegisterDto.Username?.ToLower();
            if(await _repo.UserExists(userForRegisterDto.Username))
            return BadRequest("User already exists.");

            var user = new User(){
                Username = userForRegisterDto.Username 
            };

            var userCreated = await _repo.Register(user, userForRegisterDto.Password);

            return StatusCode(201);
        }
    }
}