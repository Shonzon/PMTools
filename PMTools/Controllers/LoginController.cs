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
            if (userExists.Result!=null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new Response { Status = "Error", Message = "User already exists" });
            }

            IdentityUser identityUser = new IdentityUser
            {
                Email = registerUser.Email,
                UserName = registerUser.UserName,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            if (await _userRole.RoleExistsAsync(Role))
            {
                var result = await _userManager.CreateAsync(identityUser, registerUser.Password);
                if (!result.Succeeded)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "user creation failed" });
                }
                await _userManager.AddToRoleAsync(identityUser,Role); 
                return StatusCode(StatusCodes.Status200OK, new Response { Status = "Success", Message = "User Created Successfully" });
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Role Does not exists" });
            }
            

        }
    }
}
