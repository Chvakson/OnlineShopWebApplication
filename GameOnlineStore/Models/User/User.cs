namespace GameOnlineStore.Models
{
    public class User
    {
        public Guid Id { get;}
        public string Login { get; set; }
        public string Password { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
