﻿using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Areas.Admin.Models
{
    public class RegisterDetails
    {
        [Required(ErrorMessage = "Не указан Логин (Email)")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Номер телефона")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Не указан повторный пароль")]
        public string ConfirmPassword { get; set; }
        //[Required(ErrorMessage = "Не указано имя пользователя")]
        //public string FirstName { get; set; }
        //[Required(ErrorMessage = "Не указана фамилия пользователя")]
        //public string LastName { get; set; }
    }
}
