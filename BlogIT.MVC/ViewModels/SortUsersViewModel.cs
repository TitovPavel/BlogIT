using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class SortUsersViewModel
    {
        public SortStateUser NameSort { get; private set; }
        public SortStateUser EmailSort { get; private set; }  
        public SortStateUser SexSort { get; private set; }
        public SortStateUser BirthdaySort { get; private set; }
        public SortStateUser Current { get; private set; }   

        public SortUsersViewModel(SortStateUser sortOrder)
        {
            NameSort = sortOrder == SortStateUser.NameAsc ? SortStateUser.NameDesc : SortStateUser.NameAsc;
            EmailSort = sortOrder == SortStateUser.EmailAsc ? SortStateUser.EmailDesc : SortStateUser.EmailAsc;
            SexSort = sortOrder == SortStateUser.SexAsc ? SortStateUser.SexDesc : SortStateUser.SexAsc;
            BirthdaySort = sortOrder == SortStateUser.BirthdayAsc ? SortStateUser.BirthdayDesc : SortStateUser.BirthdayAsc;
            Current = sortOrder;
        }
    }

    public enum SortStateUser
    {
        NameAsc,
        NameDesc,
        EmailAsc,
        EmailDesc,    
        SexAsc,
        SexDesc,
        BirthdayAsc,
        BirthdayDesc
    }
}
