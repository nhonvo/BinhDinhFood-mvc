using BinhDinhFood.Application.Interfaces;
using BinhDinhFood.Presentation.Models.Report;
using Newtonsoft.Json;

namespace BinhDinhFood.Infrastructure.Repositories;

public class CartRepository : ICartRepository
{
    const string CART = "Cart";
    public List<Item> Get(ISession session)
    {
        var value = session.GetString(CART);
        if (value == null)
        {
            return new List<Item>();
        }

        var result = JsonConvert.DeserializeObject<List<Item>>(value);
        return result == null ? new List<Item>() : result;
    }

    public List<Item> Set(ISession session, List<Item> cart)
    {
        session.SetString(CART, JsonConvert.SerializeObject(cart));
        return cart;
    }
}
