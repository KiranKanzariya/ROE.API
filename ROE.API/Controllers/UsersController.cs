using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROE.API.Model;
using ROE.DataAccess.DTO;
using ROE.DataAccess.Entities;
using ROE.Services.Contracts;

namespace ROE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        [Route("FetchAllUsers/{customerId}")]
        public IActionResult FetchAllUsers(int customerId)
        {
            List<User> users = _userServices.FetchAllUsers(customerId);
            if (users?.Any() ?? false)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("FetchAllProductRole")]
        public IActionResult FetchAllProductRole()
        {
            List<Product_Role> roles = _userServices.FetchAllProductRole();
            if (roles?.Any() ?? false)
            {
                return Ok(roles);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("GetUserByEmail")]
        public IActionResult GetUserByEmail([FromBody] LoginModel model)
        {
            UserDTOModel user = _userServices.GetUserByUserName(model.UserName);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
