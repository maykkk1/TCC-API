using Gerenciador.Domain.Entities;
using Gerenciador.Domain.Interfaces;
using Gerenciador.Service.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gerenciador.Application.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public int Create([FromBody] User user)
        {
            return _userService.Add<UserValidator>(user).Id;
        }
        
        [HttpGet]
        [Authorize]
        public IList<User> Get()
        {
            return _userService.Get();
        }
    }
}
