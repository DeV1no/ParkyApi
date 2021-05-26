using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Parky.DTO;
using Parky.Entity;
using Parky.Repository.IRepository;

namespace Parky.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;

        public UserController(IUserRepository repository, IMapper mapper)
        {
            _userRepository = repository;
            _mapper = mapper;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] UserLoginDTO model)
        {
            var userobj = _mapper.Map<User>(model);
            var user = _userRepository.Authenticate(userobj.Username, userobj.Password);
            if (user == null)
                return BadRequest(new {message = "Username or password is incorrect"});
            return Ok(user);
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] UserLoginDTO model)
        {
            return Ok(_userRepository.Register(model));
        }
    }
}