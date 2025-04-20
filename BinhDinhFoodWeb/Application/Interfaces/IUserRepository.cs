using BinhDinhFood.Presentation.Models.Authentication;

namespace BinhDinhFood.Application.Interfaces;

public interface IUserRepository
{
    public CookieUserItem RegisterAsync(RegisterViewModel model);
    public CookieUserItem Validate(LoginViewModel model);
    public Task<bool> HaveAccountAsync(ForgotViewModel model);
    public Task<bool> HaveAccountAsync(string userName, string password);
    public Task ResetPasswordAsync(ResetViewModel model);
    public string CreateResetPasswordLink(string customerUserName);
    public Task ChangeInfoUserAsync(InfoViewModel model, int id, IFormFileCollection files);
    public Task ClearImageAsync(int id);
    public Task ChangePasswordUserAsync(ChangePasswordViewModel model, int id);
    public InfoViewModel GetUserInfo(int id);
}

