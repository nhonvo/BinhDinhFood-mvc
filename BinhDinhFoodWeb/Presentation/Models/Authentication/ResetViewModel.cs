using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace BinhDinhFood.Presentation.Models.Authentication;

public class ResetViewModel
{
    [Required]
    [Display(Name = "UserName")]
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
    public string Token { get; set; }
}
