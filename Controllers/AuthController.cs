using LoginRegistrationJWT.Dto;
using LoginRegistrationJWT.Interfaces;
using LoginRegistrationJWT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LoginRegistrationJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        
        [HttpPost("register")]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> Register([FromBody] UserDto userRequest)
        {
            if (_authRepository.UserExist(userRequest.Username))
            {
                ModelState.AddModelError("", "This username is already used");
                return BadRequest();
            }
 
            _authRepository.CreatePasswordHash(userRequest.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User()
            {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Username = userRequest.Username
            };


            if(!_authRepository.RegisterUser(user)) 
            {
                ModelState.AddModelError("", "Something went wrong while saving user");
                return BadRequest();
            }
            
            return Ok(user);

        }



        [HttpPost("login")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Login([FromBody] UserDto userRequest)
        {
            if (!_authRepository.UserExist(userRequest.Username))
                return NotFound();

            var user = _authRepository.GetUser(userRequest.Username);

            if (!_authRepository.VerifyPasswordHash(userRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Incorrect password.");
            }

            string token = _authRepository.CreateToken(user);

            return Ok(token);
        }
    }
}
