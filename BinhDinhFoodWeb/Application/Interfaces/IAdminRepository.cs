using BinhDinhFood.Presentation.Models.Authentication;

namespace BinhDinhFood.Application.Interfaces;

public interface IAdminRepository
{
    public CookieUserItem Validate(LoginViewModel model);
    public Task<bool> HaveAccount(ForgotViewModel model);
    public Task<bool> HaveAccount(string userName, string password);
    public Task ResetPassWord(ResetViewModel model);
    public string CreateResetPasswordLink(string adminUserName);
    public Task ChangePasswordUser(ChangePasswordViewModel model, int id);
}
