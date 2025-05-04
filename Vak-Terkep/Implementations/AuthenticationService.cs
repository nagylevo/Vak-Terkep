using Microsoft.AspNetCore.Authentication;
using Vak_Terkep.Interfaces;
using Vak_Terkep.Implementations;
using System.Security.Claims;
using System.Text;

namespace Vak_Terkep.Implementations
{
    public class AuthenticationService : IAuthenticationServices
    {
        private IHttpContextAccessor httpContextAccessor;
        private IUserInterface userInterface;
        private IEncryptService encryptService;

        public AuthenticationService(IHttpContextAccessor httpContextAccessor, IUserInterface userInterface, IEncryptService encryptService)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userInterface = userInterface;
            this.encryptService = encryptService;
        }

        public bool IsLoggedIn =>
            httpContextAccessor.HttpContext.Session.TryGetValue("email", out byte[] value);

        public void LogOut()
        {
            httpContextAccessor.HttpContext.Session.Clear();
        }

        public bool TryLogIn(string email, string password)
        {
            

            var userInDatabase = userInterface.GetAll()
            .FirstOrDefault(x => x.emailAddress == email);

            if (userInDatabase is null)
            {
                return false;
            }

            string hashedPassword = encryptService.HashPassword(password);
            if (hashedPassword != userInDatabase.userPassword)
            {
                return false;
            }

            if (httpContextAccessor.HttpContext.Session.TryGetValue("email", out byte[] value))
            {
                return false;
            }

            httpContextAccessor.HttpContext.Session.Set("email", Encoding.UTF8.GetBytes(email));
            httpContextAccessor.HttpContext.Session.SetString("userName", userInDatabase.userName);
            httpContextAccessor.HttpContext.Session.SetString("profilePictureUrl", userInDatabase.ProfilePicturePath ?? "/userImage.png");

            return true;
        }
    }
}
