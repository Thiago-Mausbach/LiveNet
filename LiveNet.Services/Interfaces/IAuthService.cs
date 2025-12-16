using LiveNet.Domain;

namespace LiveNet.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginViewModel model);
    }
}