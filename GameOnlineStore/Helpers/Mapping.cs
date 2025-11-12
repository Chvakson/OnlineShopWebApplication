using GameOnlineStore.Db.Models;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameOnlineStore.Helpers
{
    public static class Mapping
    {
        public static ProductViewModel ToProductViewModel(this Product productDb)
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

        public static List<ProductViewModel> ToProductViewModels(this List<Product> productsDb)
        {
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb) {
                productViewModels.Add(ToProductViewModel(product));
            }
            return productViewModels;
        }

        public static List<CartItemViewModel> ToCartItemViewModels(this List<CartItem> cartDbItems)
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

        public static CartViewModel ToCartViewModel(this Cart cartDb)
        {
            if (cartDb == null)
                return null;

            return new CartViewModel{
                Id = cartDb.Id,
                UserId = cartDb.UserId,
                Items = ToCartItemViewModels(cartDb.Items)
            };
        }

        public static OrderViewModel ToOrderViewModel(this Order orderDb)
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

        public static List<OrderViewModel> ToOrderViewModels(this List<Order> ordersDb)
        {
            var orderViewModels = new List<OrderViewModel>();
            foreach (var order in ordersDb)
            {
                orderViewModels.Add(ToOrderViewModel(order));
            }
            return orderViewModels;
        }

        public static UserDeliveryInfoViewModel ToUserDeliveryInfoViewModel(this UserDeliveryInfo userDeliveryInfo)
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

        public static UserAddressViewModel ToUserAddressViewModel(this UserAddress userAddress)
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

        public static Order ToOrderDbModel(this OrderViewModel orderViewModel, Cart existingCart)
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
