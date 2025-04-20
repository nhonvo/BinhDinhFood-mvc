using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BinhDinhFood.Presentation.Models.Authentication;

public class RegisterViewModel
{
    [DisplayName("Họ tên")]
    public string FullName { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Tài khoản")]
    public string UserName { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Mật khẩu")]
    public string Password { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Nhập lại mật khẩu")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
    [Required]
    [DisplayName("Email")]
    public string Email { get; set; }
    [DisplayName("Địa chỉ")]
    public string Address { get; set; }
}
public class LoginViewModel
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }
    public bool RememberLogin { get; set; }
}
public class CookieUserItem
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
public class ForgotViewModel
{
    [Required]
    [Display(Name = "UserName")]
    public string UserName { get; set; }
    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
public class InfoViewModel
{
    [DisplayName("Họ tên")]
    public string FullName { get; set; }
    [DisplayName("Số điện thoại")]
    public string? Phone { get; set; }
    [DisplayName("Email")]
    public string Email { get; set; }
    [DisplayName("Địa chỉ")]
    public string Address { get; set; }
    [DisplayName("Hình đại diện")]
    public string? Image { get; set; }
}
public class ChangePasswordViewModel
{
    [Required]
    [StringLength(50)]
    [DisplayName("Mật khẩu cũ")]
    public string OldPassword { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Mật khẩu mới")]
    public string NewPassword { get; set; }
    [Required]
    [StringLength(50)]
    [DisplayName("Nhập lại mật khẩu mới")]
    [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
