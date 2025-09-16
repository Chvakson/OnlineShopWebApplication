using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Description { get; set; }

        public string? ImgPath { get; set; }
    }
}
