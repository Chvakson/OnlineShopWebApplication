using GameOnlineStore.Db.Models;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameOnlineStore.Helpers
{
    public class Mapping
    {
        public static ProductViewModel ToProductViewModel(Product productDb)
        {
            return new ProductViewModel()
            {
                Id = productDb.Id,
                Name = productDb.Name,
                Cost = productDb.Cost,
                Description = productDb.Description,
                ImgFileName = productDb.ImgFileName,
            };
        }

        public static List<ProductViewModel> ToProductViewModels(List<Product> productsDb)
        {
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb) {
                productViewModels.Add(ToProductViewModel(product));
            }
            return productViewModels;
        }

        public static List<CartItemViewModel> ToCartItemViewModels(List<CartItem> cartDbItems)
        {
            List<CartItemViewModel> cartItemViewModels = new();
            foreach (var item in cartDbItems)
            {
                var carItemViewModel = new CartItemViewModel()
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    Product = ToProductViewModel(item.Product)
                };
                cartItemViewModels.Add(carItemViewModel);
            }
            return cartItemViewModels;
        }

        public static CartViewModel ToCartViewModel(Cart cartDb)
        {
            if (cartDb == null)
                return null;

            return new CartViewModel{
                Id = cartDb.Id,
                UserId = cartDb.UserId,
                Items = ToCartItemViewModels(cartDb.Items)
            };
        }

        public static OrderViewModel ToOrderViewModel(Order orderDb)
        {
            if (orderDb == null)
                return null;

            return new OrderViewModel
            {
                Id = orderDb.Id,
                Items = ToCartItemViewModels(orderDb.Items),
                UserDeliveryInfo = ToUserDeliveryInfoViewModel(orderDb.UserDeliveryInfo),
                Status = (OrderStatusViewModel)(int)orderDb.Status,
                CreatedDateTime = orderDb.CreatedDateTime.ToLocalTime(),
            };
        }

        public static List<OrderViewModel> ToOrderViewModels(List<Order> ordersDb)
        {
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in ordersDb)
            {
                orderViewModels.Add(ToOrderViewModel(order));
            }
            return orderViewModels;
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(UserDeliveryInfo userDeliveryInfo)
        {

            return new UserDeliveryInfoViewModel
            {
                Id = userDeliveryInfo.Id,
                Email = userDeliveryInfo.Email, 
                FirstName = userDeliveryInfo.FirstName, 
                LastName = userDeliveryInfo.LastName,
                Phone = userDeliveryInfo.Phone,
                Comment = userDeliveryInfo.Comment,
                UserAddress = ToUserAddressViewModel(userDeliveryInfo.Address)
            };
        }

        public static UserAddressViewModel ToUserAddressViewModel(UserAddress userAddress)
        {
            return new UserAddressViewModel
            {
                Id = userAddress.Id,
                City = userAddress.City,
                Street = userAddress.Street,
                Entrance = userAddress.Entrance,
                HomeNo = userAddress.HomeNo,
                FloorNo = userAddress.FloorNo,
                FlatNo = userAddress.FlatNo
            };
        }

        public static List<SelectListItem> GetStatusList()
        {
            return Enum.GetValues(typeof(OrderStatusViewModel))
                .Cast<OrderStatusViewModel>()
                .Select(s => new SelectListItem
                {
                    Value = ((int)s).ToString(),
                    Text = EnumHelper.GetDisplayName(s)
                })
                .ToList();
        }

        public static Order ToOrderDbModel(OrderViewModel orderViewModel, Cart existingCart)
        {
            return new Order
            {
                CreatedDateTime = DateTime.UtcNow,
                Status = (OrderStatus)orderViewModel.Status,
                UserDeliveryInfo = new UserDeliveryInfo
                {
                    Email = orderViewModel.UserDeliveryInfo.Email,
                    FirstName = orderViewModel.UserDeliveryInfo.FirstName,
                    LastName = orderViewModel.UserDeliveryInfo.LastName,
                    Phone = orderViewModel.UserDeliveryInfo.Phone,
                    Comment = orderViewModel.UserDeliveryInfo.Comment,
                    Address = new UserAddress
                    {
                        City = orderViewModel.UserDeliveryInfo.UserAddress.City,
                        Street = orderViewModel.UserDeliveryInfo.UserAddress.Street,
                        Entrance = orderViewModel.UserDeliveryInfo.UserAddress.Entrance,
                        HomeNo = orderViewModel.UserDeliveryInfo.UserAddress.HomeNo,
                        FloorNo = orderViewModel.UserDeliveryInfo.UserAddress.FloorNo,
                        FlatNo = orderViewModel.UserDeliveryInfo.UserAddress.FlatNo
                    }
                },
                Items = existingCart.Items.ToList()
            };
        }
    }
}
