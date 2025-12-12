using LiveNet.Domain.ViewModels;

namespace LiveNet.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginViewModel model);
    }
}
