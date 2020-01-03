using System;
using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "UserName", Prompt = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Email", Prompt = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday", Prompt = "Birthday")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Sex", Prompt = "Sex")]
        public string Sex { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "PasswordConfirm", Prompt = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }

    }
}
