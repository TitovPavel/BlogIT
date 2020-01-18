using System;
using System.ComponentModel.DataAnnotations;


namespace BlogIT.MVC.ViewModels
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }
        [Display(Name = "Sex")]
        public string Sex { get; set; }
    }
}
