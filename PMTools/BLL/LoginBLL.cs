using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PMTools.Interface;
using PMTools.Models;
using PMTools.Models.Authentication;
using PMTools.Models.Authentication.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMTools.BLL
{
    public class LoginBLL:ILogin
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _userRole;
        private readonly IConfiguration configuration;

        public LoginBLL(UserManager<IdentityUser> usermanager, RoleManager<IdentityRole> userrole, IConfiguration config)
        {
            this._userManager = usermanager;
            this._userRole = userrole;
            this.configuration = config;
        }

        public async Task<Response> GenerateToken(string UserName, string Password)
        {
            var user = await _userManager.FindByNameAsync(UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, Password))
            {
                // user is valid
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                //If you've had the login module, you can also use the real user information here
                IdentityOptions _options = new IdentityOptions();
                var claims = new List<Claim>
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.UserNameClaimType, user.UserName)
                    };
                var userClaims = await _userManager.GetClaimsAsync(user);
                var userRoles = await _userManager.GetRolesAsync(user);
                claims.AddRange(userClaims);
                foreach (var userRole in userRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole));
                    var role = await _userRole.FindByNameAsync(userRole);
                    if(role != null)
                    {
                        var roleClaims = await _userRole.GetClaimsAsync(role);
                        foreach(Claim roleClaim in roleClaims)
                        {
                            claims.Add(roleClaim);
                        }
                    }
                }
                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials);
                

                return new Response { Status = "Success", Message = new JwtSecurityTokenHandler().WriteToken(token) } ;
            }
            else
            {
                return new Response { Status = "Error", Message = "No User Found" };
            }
        }

        public async Task<Response> RegisterNewUser(RegisterUser registerUser, string Role)
        {
            var userExists = _userManager.FindByEmailAsync(registerUser.Email);
            if (userExists.Result != null)
            {
                return new Response { Status = "Error", Message = "User already exists" };
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
                    return new Response { Status = "Error", Message = "user creation failed" };
                }
                await _userManager.AddToRoleAsync(identityUser, Role);
                return new Response { Status = "Success", Message = "User Created Successfully" };
            }
            else
            {
                return new Response { Status = "Error", Message = "Role Does not exists" };
            }
        }

    }
}
