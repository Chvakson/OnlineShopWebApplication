namespace GameOnlineStore.Db.Models
{
    public class UserAddress
    {
        public Guid Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? Entrance { get; set; }
        public string HomeNo { get; set; }
        public string? FloorNo { get; set; }
        public string? FlatNo { get; set; }
    }
}
