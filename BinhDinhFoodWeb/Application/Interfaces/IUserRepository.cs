using BinhDinhFood.Presentation.Models.Authentication;

namespace BinhDinhFood.Application.Interfaces;

public interface IUserRepository
{
    public CookieUserItem Register(RegisterViewModel model);
    public CookieUserItem Validate(LoginViewModel model);
    public Task<bool> HaveAccount(ForgotViewModel model);
    public Task<bool> HaveAccount(string userName, string password);
    public Task ResetPassWord(ResetViewModel model);
    public string CreateResetPasswordLink(string customerUserName);
    public Task ChangeInforUser(InforViewModel model, int id, IFormFileCollection files);
    public Task ClearImage(int id);
    public Task ChangePasswordUser(ChangePasswordViewModel model, int id);
    public InforViewModel GetUserInfor(int id);
}

