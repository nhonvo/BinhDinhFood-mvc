using Microsoft.AspNetCore.SignalR;

namespace BinhDinhFood.Infrastructure.Hubs;

public class CustomerHub : Hub
{
    public static int _customerCounter = 0;
    public override Task OnConnectedAsync()
    {
        _customerCounter++;
        Clients.All.SendAsync("updateCustomerCounter", _customerCounter).GetAwaiter().GetResult();
        return base.OnConnectedAsync();
    }
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _customerCounter--;
        Clients.All.SendAsync("updateCustomerCounter", _customerCounter).GetAwaiter().GetResult();
        return base.OnDisconnectedAsync(exception);
    }
    public static int CustomerCount
    {
        get => _customerCounter;
        set => _customerCounter = value;
    }
}
