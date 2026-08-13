using System.Net;
using System.Text;
using GameOnlineShop.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace GameOnlineShop.Services
{
    public class OrderEmailService : IOrderEmailService
    {
        private readonly EmailSettings settings;
        private readonly IWebHostEnvironment environment;
        private readonly ILogger<OrderEmailService> logger;

        public OrderEmailService(
            IOptions<EmailSettings> settings,
            IWebHostEnvironment environment,
            ILogger<OrderEmailService> logger)
        {
            this.settings = settings.Value;
            this.environment = environment;
            this.logger = logger;
        }

        public async Task<OrderEmailSendResult> SendOrderConfirmationAsync(OrderViewModel order)
        {
            var email = order.UserDeliveryInfo?.Email;
            if (string.IsNullOrWhiteSpace(email))
            {
                logger.LogWarning("Не указан email получателя, письмо о заказе не отправлено.");
                return new OrderEmailSendResult(false, "Не указан email получателя.");
            }

            var message = BuildMessage(order, email);

            try
            {
                if (!HasSmtpCredentials())
                {
                    await SaveToPickupDirectoryAsync(message);
                    logger.LogWarning(
                        "SMTP не настроен (Email:Password). Письмо о заказе {OrderId} сохранено в {Folder}",
                        order.Id,
                        settings.PickupDirectory);
                    return new OrderEmailSendResult(
                        false,
                        "Gmail не отправляет письма без пароля приложения. Создайте его в аккаунте Google и вставьте в Email:Password в appsettings.Development.json, затем перезапустите приложение.");
                }

                await SendViaSmtpAsync(message);
                logger.LogInformation("Письмо о заказе {OrderId} отправлено на {Email}", order.Id, email);
                return new OrderEmailSendResult(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Не удалось отправить письмо о заказе {OrderId} на {Email}", order.Id, email);
                try
                {
                    await SaveToPickupDirectoryAsync(message);
                }
                catch (Exception saveEx)
                {
                    logger.LogError(saveEx, "Не удалось сохранить письмо о заказе {OrderId} на диск", order.Id);
                }

                return new OrderEmailSendResult(false, SimplifySmtpError(ex));
            }
        }

        private bool HasSmtpCredentials()
        {
            return !string.IsNullOrWhiteSpace(settings.SmtpHost) &&
                   !string.IsNullOrWhiteSpace(settings.UserName) &&
                   !string.IsNullOrWhiteSpace(settings.Password);
        }

        private MimeMessage BuildMessage(OrderViewModel order, string email)
        {
            var fromAddress = string.IsNullOrWhiteSpace(settings.FromAddress)
                ? settings.UserName
                : settings.FromAddress;

            if (string.IsNullOrWhiteSpace(fromAddress))
            {
                fromAddress = "noreply@gameshop.local";
            }

            var shortId = order.Id.ToString("N")[..8].ToUpperInvariant();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(settings.FromName, fromAddress));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = $"Заказ #{shortId} оформлен — GameShop";
            message.Body = new TextPart("html")
            {
                Text = BuildHtmlBody(order, shortId)
            };

            return message;
        }

        private async Task SendViaSmtpAsync(MimeMessage message)
        {
            var password = settings.Password.Replace(" ", "", StringComparison.Ordinal);
            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(settings.SmtpHost, settings.SmtpPort,
                    settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto);
                await client.AuthenticateAsync(settings.UserName, password);
                await client.SendAsync(message);
            }
            finally
            {
                if (client.IsConnected)
                {
                    await client.DisconnectAsync(true);
                }
            }
        }

        private async Task SaveToPickupDirectoryAsync(MimeMessage message)
        {
            var folder = Path.IsPathRooted(settings.PickupDirectory)
                ? settings.PickupDirectory
                : Path.Combine(environment.ContentRootPath, settings.PickupDirectory);

            Directory.CreateDirectory(folder);
            var filePath = Path.Combine(folder, $"{DateTime.UtcNow:yyyyMMddHHmmss}_{message.MessageId?.Replace("<", "").Replace(">", "") ?? Guid.NewGuid().ToString("N")}.eml");
            await using var stream = File.Create(filePath);
            await message.WriteToAsync(stream);
        }

        private static string SimplifySmtpError(Exception ex)
        {
            var text = ex.InnerException?.Message ?? ex.Message;
            if (text.Contains("5.7.8", StringComparison.OrdinalIgnoreCase) ||
                text.Contains("Invalid credentials", StringComparison.OrdinalIgnoreCase) ||
                text.Contains("Authentication", StringComparison.OrdinalIgnoreCase))
            {
                return "Gmail отклонил логин или пароль. Нужен пароль приложения (16 символов), а не обычный пароль от почты. Включите двухэтапную проверку и создайте пароль: https://myaccount.google.com/apppasswords";
            }

            return text;
        }

        private static string BuildHtmlBody(OrderViewModel order, string shortId)
        {
            var info = order.UserDeliveryInfo;
            var address = info.UserAddress;
            var created = order.CreatedDateTime == default
                ? DateTime.Now
                : order.CreatedDateTime.ToLocalTime();

            var itemsHtml = new StringBuilder();
            foreach (var item in order.Items ?? [])
            {
                var name = WebUtility.HtmlEncode(item.Product?.Name ?? "Игра");
                itemsHtml.Append($"""
                    <tr>
                        <td style="padding:10px 12px;border-bottom:1px solid #1c3d46;">{name}</td>
                        <td style="padding:10px 12px;border-bottom:1px solid #1c3d46;text-align:center;">{item.Amount}</td>
                        <td style="padding:10px 12px;border-bottom:1px solid #1c3d46;text-align:right;">{item.Product?.Cost.ToString("N0")} ₽</td>
                        <td style="padding:10px 12px;border-bottom:1px solid #1c3d46;text-align:right;font-weight:700;">{item.Cost.ToString("N0")} ₽</td>
                    </tr>
                    """);
            }

            var fullAddress = string.Join(", ", new[]
            {
                address?.City,
                string.IsNullOrWhiteSpace(address?.Street) ? null : $"ул. {address.Street}",
                string.IsNullOrWhiteSpace(address?.HomeNo) ? null : $"д. {address.HomeNo}",
                string.IsNullOrWhiteSpace(address?.Entrance) ? null : $"подъезд {address.Entrance}",
                string.IsNullOrWhiteSpace(address?.FloorNo) ? null : $"этаж {address.FloorNo}",
                string.IsNullOrWhiteSpace(address?.FlatNo) ? null : $"кв. {address.FlatNo}"
            }.Where(part => !string.IsNullOrWhiteSpace(part)));

            var comment = string.IsNullOrWhiteSpace(info.Comment)
                ? "—"
                : WebUtility.HtmlEncode(info.Comment);

            return $"""
                <div style="background:#061318;padding:24px;font-family:Arial,sans-serif;color:#e8f6f8;">
                  <div style="max-width:640px;margin:0 auto;background:#0d2730;border:1px solid #1c3d46;border-radius:16px;overflow:hidden;">
                    <div style="padding:20px 24px;background:#155e75;">
                      <h1 style="margin:0;font-size:22px;color:#f0fdff;">GameShop</h1>
                      <p style="margin:8px 0 0;color:#c5eef5;">Заказ #{shortId} успешно оформлен</p>
                    </div>
                    <div style="padding:24px;">
                      <p>Здравствуйте, {WebUtility.HtmlEncode(info.FirstName)} {WebUtility.HtmlEncode(info.LastName)}!</p>
                      <p>Спасибо за покупку. Мы получили ваш заказ {created:dd.MM.yyyy HH:mm}.</p>
                      <h2 style="font-size:16px;margin:24px 0 12px;color:#67e8f9;">Состав заказа</h2>
                      <table style="width:100%;border-collapse:collapse;color:#e8f6f8;">
                        <thead>
                          <tr>
                            <th style="text-align:left;padding:10px 12px;border-bottom:1px solid #1c3d46;">Игра</th>
                            <th style="padding:10px 12px;border-bottom:1px solid #1c3d46;">Кол-во</th>
                            <th style="text-align:right;padding:10px 12px;border-bottom:1px solid #1c3d46;">Цена</th>
                            <th style="text-align:right;padding:10px 12px;border-bottom:1px solid #1c3d46;">Сумма</th>
                          </tr>
                        </thead>
                        <tbody>
                          {itemsHtml}
                        </tbody>
                      </table>
                      <p style="text-align:right;font-size:20px;font-weight:700;margin-top:16px;">Итого: {order.Cost.ToString("N0")} ₽</p>
                      <h2 style="font-size:16px;margin:24px 0 12px;color:#67e8f9;">Доставка</h2>
                      <p style="margin:0 0 8px;"><strong>Адрес:</strong> {WebUtility.HtmlEncode(fullAddress)}</p>
                      <p style="margin:0 0 8px;"><strong>Телефон:</strong> {WebUtility.HtmlEncode(info.Phone)}</p>
                      <p style="margin:0 0 8px;"><strong>Email:</strong> {WebUtility.HtmlEncode(info.Email)}</p>
                      <p style="margin:0;"><strong>Комментарий:</strong> {comment}</p>
                    </div>
                  </div>
                </div>
                """;
        }
    }
}
