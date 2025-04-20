using Microsoft.AspNetCore.SignalR;
namespace BinhDinhFood.Infrastructure.Hubs;

public class AdminHub : Hub
{

    public static int _customerCounter;
    public override Task OnConnectedAsync()
    {
        _customerCounter = CustomerHub.CustomerCount;
        Clients.All.SendAsync("updateCustomerCounter", _customerCounter).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }
}
