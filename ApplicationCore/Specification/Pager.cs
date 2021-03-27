using ApplicationCore.Constants;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specification
{
    public class Pager
    {
        //1000, 1, 5, 5
        public Pager(int totalItems, int currentPage, int sizePage, int maxPages = PaginationConstants.DefaultMaxPage)
        {
            //validate the page size and setting the default value
            if (sizePage < 1)
                sizePage = PaginationConstants.DefaultPageSize;

            //Calculate Total Pages
            var totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)sizePage);

            // Enseure current page isn't out of range
            if (currentPage < 1)
            {
                currentPage = 1;
            }
            else if (currentPage > totalPages)
            {
                currentPage = totalPages;
            }

            int startPage, endPage;
            if(totalPages <= maxPages)
            {
                // Total pages less than max so show all pages
                startPage = 1;
                endPage = totalPages;
            } else
            {
                // Total pages more than max so calculare start and end pages
                var maxPagesBeforeCurrentPage = (int)Math.Floor((decimal)maxPages / (decimal)2);
                var maxPagesAfterCurrentPage = (int)Math.Ceiling((decimal)maxPages / (decimal)2) - 1;
                if(currentPage <= maxPagesBeforeCurrentPage)
                {
                    //current page near the start
                    startPage = 1;
                    endPage = maxPages;
                } else if (currentPage + maxPagesAfterCurrentPage >= totalPages)
                {
                    // current page near the end
                    startPage = totalPages - maxPages + 1;
                    endPage = totalPages;
                } else
                {
                    // current page somewhere in the middle
                    startPage = currentPage - maxPagesBeforeCurrentPage;
                    endPage = currentPage + maxPagesAfterCurrentPage;
                }
            }

            //calculate start and end item indexes
            var startIndex = (currentPage - 1) * sizePage;
            var endIndex = Math.Min(startIndex + sizePage + 1, totalItems - 1);

            //create an array of pages that can be looped over;
            var pages = Enumerable.Range(startPage, (endPage + 1) - startPage);

            var nextPage = currentPage + 1;
            var previousPage = currentPage - 1;

            //Update object instante with all pager properties required view client
            TotalItems = totalItems;
            CurrentPage = currentPage;
            SizePage = sizePage;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Pages = pages;
            NextPage = nextPage;
            PreviousPage = previousPage;
        }

        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int SizePage { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public int NextPage { get; private set; }
        public int PreviousPage { get; private set; }
        public IEnumerable<int> Pages { get; private set; }
    }
}
