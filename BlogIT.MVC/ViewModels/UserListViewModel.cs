using System.Collections.Generic;

namespace BlogIT.MVC.ViewModels
{
    public class UserListViewModel
    {
        public List<UserViewModel> UserViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortUsersViewModel SortUsersViewModel { get; set; }
        public FilterUsersViewModel FilterUsersViewModel { get; set; }
    }
}
