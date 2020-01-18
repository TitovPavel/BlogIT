namespace BlogIT.MVC.ViewModels
{
    public class FilterUsersViewModel
    {
        public FilterUsersViewModel(string searchString)
        {
            SearchString = searchString;
        }

        public string SearchString { get; set; }
    }
}
