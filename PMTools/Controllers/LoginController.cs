using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PMTools.Models;
using PMTools.Models.Authentication.Login;

namespace PMTools.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRole;
        private readonly IConfiguration configuration;


        public LoginController(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> userrole, IConfiguration config)
        {
            this._userManager = usermanager;
            this._userRole = userrole;
            this.configuration = config;

        }
        [HttpPost]
        [Route("RegisterNewUser")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUser registerUser, string Role)
        {
            var userExists = _userManager.FindByEmailAsync(registerUser.Email);
            if (userExists!=null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists" });
            }

            IdentityUser identityUser = new IdentityUser
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result=await _userManager.CreateAsync(identityUser,registerUser.Password);
            if (result.Succeeded)
            {
                return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User Created Successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "user creation failed" });
            }

        }
    }
}
