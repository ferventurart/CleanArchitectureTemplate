using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Constants;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specification;
using ApplicationCore.Specification.Filters;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Areas.Alumnos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IRepository<Alumno> _repository;
        private INotyfService _notyfService { get; }

        public IndexModel(IRepository<Alumno> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        public List<Alumno> Alumnos { get; set; }
        public int[] DefaultPagesSizes => PaginationHelper.DefaultPagesSizes;
        public Pager Pager { get; set; }
        public string SearchString { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public async Task OnGetAsync(string searchString, int? currentPage, int? pageSize)
        {
            PageSize = pageSize.HasValue ? pageSize.Value : PaginationConstants.DefaultPageSize;

            if (!string.IsNullOrEmpty(searchString))
            {
                SearchString = searchString;
                TotalItems = await _repository.CountAsync(new AlumnoSpec(new AlumnoFilter { Apellido = searchString, IsPagingEnabled = false }));
            }
            else
            {
                TotalItems = await _repository.CountAsync(new AlumnoSpec(new AlumnoFilter { IsPagingEnabled = false }));
            }

            Pager = new Pager(TotalItems, currentPage.HasValue ? currentPage.Value : PaginationConstants.DefaultPage, PageSize);

            var filter = new AlumnoFilter();
            filter.IsPagingEnabled = true;
            filter.Apellido = searchString;
            filter.PageSize = Pager.PageSize;
            filter.Page = Pager.CurrentPage;

            Alumnos = await _repository.ListAsync(new AlumnoSpec(filter));
        }
    }
}
