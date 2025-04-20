using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Infrastructure;

namespace BinhDinhFood.Infrastructure.Repositories;

public class TokenRepository : ITokenRepository
{
    private readonly BinhDinhFoodDbContext _context;
    public TokenRepository(BinhDinhFoodDbContext context)
    {
        _context = context;
    }

    public bool CheckToken(string userName, string token)
    {
        return _context.Tokens.FirstOrDefault(Token => Token.CustomerUserName == userName && Token.TokenValue == token && Token.Expiry > DateTime.Now) != null;
    }
}
