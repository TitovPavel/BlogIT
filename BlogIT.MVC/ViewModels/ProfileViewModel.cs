using System;
using System.ComponentModel.DataAnnotations;

namespace BlogIT.MVC.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        public int AvatarID { get; set; }
        public bool AvatarExist { get; set; }
        [Display(Name = "DateOfRegistration")]
        public DateTime DateOfRegistration { get; set; }
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }

    }
}
