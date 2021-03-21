using ApplicationCore.Entities;
using ApplicationCore.Specification.Filters;
using Ardalis.Specification;

namespace ApplicationCore.Specification
{
    public class AlumnoSpec : Specification<Alumno>
    {
        public AlumnoSpec(AlumnoFilter filter)
        {
            Query.OrderBy(x => x.Apellido).ThenByDescending(x => x.Id);

            if (filter.IsPagingEnabled)
                Query.Skip(PaginationHelper.CalculateSkip(filter))
                     .Take(PaginationHelper.CalculateTake(filter));

            if (!string.IsNullOrEmpty(filter.Apellido))
                Query.Search(x => x.Apellido, "%" + filter.Apellido + "%");
        }
    }
}
