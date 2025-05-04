namespace Vak_Terkep.Interfaces
{
    public interface IAuthenticationServices
    {
        void LogOut();
        bool TryLogIn(string email, string password);
        bool IsLoggedIn { get; }
    }
}
