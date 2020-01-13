using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIT.MVC.ViewModels
{
    public class SortNewsViewModel
    {
        public SortState TitleSort { get; private set; }
        public SortState DateTimeSort { get; private set; }  
        public SortState WriterSort { get; private set; } 
        public SortState Current { get; private set; }   

        public SortNewsViewModel(SortState sortOrder)
        {
            TitleSort = sortOrder == SortState.TitleAsc ? SortState.TitleDesc : SortState.TitleAsc;
            DateTimeSort = sortOrder == SortState.DateTimeAsc ? SortState.DateTimeDesc : SortState.DateTimeAsc;
            WriterSort = sortOrder == SortState.WriterAsc ? SortState.WriterDesc : SortState.WriterAsc;
            Current = sortOrder;
        }
    }

    public enum SortState
    {
        TitleAsc,   
        TitleDesc,
        DateTimeAsc,
        DateTimeDesc,    
        WriterAsc, 
        WriterDesc 
    }
}
