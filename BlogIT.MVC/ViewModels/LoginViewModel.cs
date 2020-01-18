using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "RememberMe")]
        public bool RememberMe { get; set; }
    }
}
