using ApplicationCore.Constants;
using ApplicationCore.Specification.Filters;

namespace ApplicationCore.Specification
{
    public static class PaginationHelper
    {
        public static readonly int[] DefaultSizesPages = { 5, 10, 25, 50, 100 };

        public static int CalculateTake(int pageSize)
        {
            return pageSize <= 0 ? PaginationConstants.DefaultPageSize : pageSize;
        }

        public static int CalculateSkip(int pageSize, int page)
        {
            page = page <= 0 ? PaginationConstants.DefaultPage : page;

            return CalculateTake(pageSize) * (page - 1);
        }

        public static int CalculateTake(BaseFilter baseFilter)
        {
            return CalculateTake(baseFilter.SizePage);
        }

        public static int CalculateSkip(BaseFilter baseFilter)
        {
            return CalculateSkip(baseFilter.SizePage, baseFilter.Page);
        }
    }
}
