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
using Infraestructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Areas.Alumnos.Pages
{
    public class IndexModel : PageModel
    {
        private readonly MyRepository<Alumno> _repository;
        private INotyfService _notyfService { get; }

        public IndexModel(MyRepository<Alumno> repository, INotyfService notyfService)
        {
            _repository = repository;
            _notyfService = notyfService;
        }

        public List<Alumno> Alumnos { get; set; }
        public UIPaginationModel UIPagination { get; set; }

        public async Task OnGetAsync(string searchString, int? currentPage, int? pageSize)
        {
            var totalItems = await _repository.CountAsync(new AlumnoSpec(new AlumnoFilter { Apellido = searchString, LoadChildren = false, IsPagingEnabled = true }));
            UIPagination = new UIPaginationModel(currentPage, searchString, pageSize, totalItems);

            Alumnos = await _repository.ListAsync(new AlumnoSpec(
                new AlumnoFilter
                {
                    IsPagingEnabled = true,
                    Apellido = UIPagination.SearchString,
                    PageSize = UIPagination.GetPageSize,
                    Page = UIPagination.GetCurrentPage
                })
             );
        }
    }
}
