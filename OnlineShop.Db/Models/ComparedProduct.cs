namespace GameOnlineStore.Db.Models
{
     public class ComparedProduct
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
    }
}
