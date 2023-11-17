using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace OrderFood.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Поле 'Email' обязательно для заполнения")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле 'Пароль' обязательно для заполнения")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле 'Подтвердить пароль' обязательно для заполнения")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}
