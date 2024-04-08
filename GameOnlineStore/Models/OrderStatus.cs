﻿using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public enum OrderStatus
    {
        [Display(Name ="Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed,
        [Display(Name = "В пути")]
        OnTheWay,
        [Display(Name = "Отменен")]
        Cancelled,
        [Display(Name = "Доставлен")]
        Delivered
    }
}
