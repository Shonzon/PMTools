using PMTools.Models;
using PMTools.Models.Authentication.Login;

namespace PMTools.Interface
{
    public interface ILogin
    {
        public Task<Response> RegisterNewUser(RegisterUser registerUser, string Role);
        public Task<Response> GenerateToken(string UserName, string Password);
    }
}
