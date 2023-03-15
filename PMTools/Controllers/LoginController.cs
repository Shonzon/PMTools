using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMTools.BLL;
using PMTools.Interface;
using PMTools.Models;
using PMTools.Models.Authentication.Login;

namespace PMTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {

        ILogin _login;

        public LoginController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> userrole, IConfiguration config)
        {
            _login = new LoginBLL(usermanager,userrole,config);
        }
        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUser registerUser, string Role)
        {

            var response =await _login.RegisterNewUser(registerUser,Role);
            if (response.Status.Equals("Success"))
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        [HttpPost]
        [Route("GenerateToken")]
        public async Task<IActionResult> GenerateToken(string UserName,string Password)
        {

            var response =await _login.GenerateToken(UserName, Password);
            if (response.Status.Equals("Success"))
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
