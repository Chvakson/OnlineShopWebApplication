using GameOnlineShop.Models;

namespace GameOnlineShop.Services
{
    public sealed record OrderEmailSendResult(bool Sent, string? Error = null);

    public interface IOrderEmailService
    {
        Task<OrderEmailSendResult> SendOrderConfirmationAsync(OrderViewModel order);
    }
}
