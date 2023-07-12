using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApplication.Models
{
    public enum OrderStatus
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Delevered
    }
}
