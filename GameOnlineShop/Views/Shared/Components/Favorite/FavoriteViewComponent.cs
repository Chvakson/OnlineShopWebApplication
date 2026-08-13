using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineShop.Repositories.FavoriteProducts;



namespace GameOnlineShop.Views.Shared.Components.FavoriteViewComponent
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly IFavoriteDbRepository favoriteDbRepository;

        public FavoriteViewComponent(IFavoriteDbRepository favoriteDbRepository)
        {
            this.favoriteDbRepository = favoriteDbRepository;
        }

        public IViewComponentResult Invoke()
        {
            var productsCount = favoriteDbRepository.GetAll(Constants.UserId);

            return View("Favorite", productsCount.Count);
        }
    }
}
