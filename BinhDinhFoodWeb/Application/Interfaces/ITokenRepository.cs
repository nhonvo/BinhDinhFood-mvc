namespace BinhDinhFood.Application.Interfaces;

public interface ITokenRepository
{
    public bool CheckToken(string userName, string token);
}
