namespace GameOnlineStore.Services
{
    public class EmailSettings
    {
        public const string SectionName = "Email";

        public string SmtpHost { get; set; } = "smtp.gmail.com";
        public int SmtpPort { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FromName { get; set; } = "GameStore";
        public string FromAddress { get; set; } = string.Empty;
        public string PickupDirectory { get; set; } = "App_Data/MailPickup";
    }
}
