using GameOnlineStore.Models;

namespace GameOnlineStore.Db.Models
{
    public class CartItemViewModel
    {
        public Guid Id { get; set; }
        public ProductViewModel Product { get; set; }
        public Cart Cart { get; set; }
        public int Amount { get; set; }

        public decimal Cost
        {
            get
            {
                return Product.Cost * Amount;
            }
        }
    }
}

