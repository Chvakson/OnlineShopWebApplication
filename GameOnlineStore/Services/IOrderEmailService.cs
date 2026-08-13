using GameOnlineStore.Models;

namespace GameOnlineStore.Services
{
    public sealed record OrderEmailSendResult(bool Sent, string? Error = null);

    public interface IOrderEmailService
    {
        Task<OrderEmailSendResult> SendOrderConfirmationAsync(OrderViewModel order);
    }
}
