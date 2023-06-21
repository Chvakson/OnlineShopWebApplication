namespace OnlineShopWebApplication.Models
{
    public class Product
    {
        private static int instanceCounter = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public int Cost { get; set; }
        public string Description { get; set; }
        public string ImgPath { get; set; }

        public Product(string name, int cost, string description, string imgPath)
        {
            Id = instanceCounter++;
            Name = name;
            Cost = cost;
            Description = description;
            ImgPath = imgPath;
        }

        public override string ToString()
        {
            return $"{Id}\n{Name}\n{Cost}";
        }
    }
}
