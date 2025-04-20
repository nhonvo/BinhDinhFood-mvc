using System.Security.Cryptography;
using System.Text;
using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Domain.Entities;
using BinhDinhFood.Infrastructure;
using BinhDinhFood.Presentation.Models.Authentication;
using Microsoft.EntityFrameworkCore;
namespace BinhDinhFood.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly BinhDinhFoodDbContext _context;

    public AdminRepository(BinhDinhFoodDbContext context)
    {
        _context = context;
    }
    private string Encode(string originalPassword)
    {
        MD5 md5 = MD5.Create();
        byte[] originalBytes = Encoding.Default.GetBytes(originalPassword);
        byte[] encodedBytes = md5.ComputeHash(originalBytes);

        return BitConverter.ToString(encodedBytes);
    }
    private string ReturnToken(int length, string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException("length", "length cannot be less than zero.");
        }

        if (string.IsNullOrEmpty(allowedChars))
        {
            throw new ArgumentException("allowedChars may not be empty.");
        }

        const int byteSize = 0x100;
        var allowedCharSet = new HashSet<char>(allowedChars).ToArray();
        if (byteSize < allowedCharSet.Length)
        {
            throw new ArgumentException(string.Format("allowedChars may contain no more than {0} characters.", byteSize));
        }

        // Guid.NewGuid and System.Random are not particularly random. By using a
        // cryptographically-secure random number generator, the caller is always
        // protected, regardless of use.
        using (var rng = RandomNumberGenerator.Create())
        {
            var result = new StringBuilder();
            var buf = new byte[128];
            while (result.Length < length)
            {
                rng.GetBytes(buf);
                for (var i = 0; i < buf.Length && result.Length < length; ++i)
                {
                    // Divide the byte into allowedCharSet-sized groups. If the
                    // random value falls into the last group and the last group is
                    // too small to choose from the entire allowedCharSet, ignore
                    // the value in order to avoid biasing the result.
                    var outOfRangeStart = byteSize - byteSize % allowedCharSet.Length;
                    if (outOfRangeStart <= buf[i])
                    {
                        continue;
                    }

                    result.Append(allowedCharSet[buf[i] % allowedCharSet.Length]);
                }
            }
            return result.ToString();
        }
    }
    private Admin GetAdmin(int id)
    {
        return _context.Admins.Find(id);
    }
    private Admin GetAdmin(string userName)
    {
        return _context.Admins.FirstOrDefault(user => user.AdminUserName == userName);
    }
    public CookieUserItem Validate(LoginViewModel model)
    {
        var userRecords = _context.Admins.Where(x => x.AdminUserName == model.UserName);

        var results = userRecords.AsEnumerable()
        .Where(m => m.AdminPassword == Encode(model.Password))
        .Select(m => new CookieUserItem
        {
            Id = m.AdminId,
            Email = m.AdminEmail,
            UserName = m.AdminUserName,
            Role = "Admin"
        });

        return results.FirstOrDefault();
    }
    public CookieUserItem Register(RegisterViewModel model)
    {
        var user = new Admin
        {
            AdminUserName = model.UserName,
            AdminPassword = Encode(model.Password),
            AdminEmail = model.Email,
            AdminImage = "avatar.jpg",
            AdminDateCreated = DateTime.Now,
        };
        _context.Admins.Add(user);
        _context.SaveChanges();

        return new CookieUserItem
        {
            Id = user.AdminId,
            UserName = user.AdminUserName,
            Email = user.AdminEmail,
            Role = "Admin"
        };
    }
    public string CreateResetPasswordLink(string adminUserName)
    {
        string token = ReturnToken(64);
        Token toKen = new Token(adminUserName, token, DateTime.Now.AddMinutes(2));
        _context.Tokens.Add(toKen);
        _context.SaveChanges();
        return "https://localhost:44316/Admin/AdmAccount/ResetPassword?user=" + adminUserName + "&token=" + token;
    }
    public async Task<bool> HaveAccount(ForgotViewModel model)
    {
        return await _context.Admins.AnyAsync(x => x.AdminUserName == model.UserName && x.AdminEmail == model.Email);
    }
    public async Task<bool> HaveAccount(string userName, string password)
    {
        return await _context.Admins.AnyAsync(_context => _context.AdminUserName == userName && _context.AdminPassword == Encode(password));
    }
    public async Task ResetPassWord(ResetViewModel model)
    {
        var Admin = GetAdmin(model.UserName);
        Admin.AdminPassword = Encode(model.Password);
        await _context.SaveChangesAsync();
    }
    public async Task ChangePasswordUser(ChangePasswordViewModel model, int id)
    {
        var user = GetAdmin(id);
        user.AdminPassword = Encode(model.NewPassword);
        await _context.SaveChangesAsync();
    }
}
