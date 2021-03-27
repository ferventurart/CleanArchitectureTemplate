using ApplicationCore.Constants;
using ApplicationCore.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class UIPaginationModel
    {
        private readonly Pager _pager;
        public int[] SizesPages => PaginationHelper.DefaultSizesPages;
        public string SearchString { get; private set; }

        public UIPaginationModel(int? currentPage, string searchString, int? sizePage, int totalItems)
        {
            _pager = new Pager(totalItems, currentPage.HasValue ? currentPage.Value : PaginationConstants.DefaultPage, sizePage.HasValue ? sizePage.Value : PaginationConstants.DefaultPageSize);
            SearchString = searchString;
        }

        public int GetStartIndex => _pager.StartIndex;
        public int GetEndIndex => _pager.EndIndex;
        public int GetNextPage => _pager.NextPage;
        public int GetPreviousPage => _pager.PreviousPage;
        public int GetCurrentPage => _pager.CurrentPage;
        public int GetTotalPages => _pager.TotalPages;
        public int GetTotalItems => _pager.TotalItems;
        public int GetSizePage => _pager.SizePage;
        public IEnumerable<int> GetPages => _pager.Pages;
    }
}
