using ApplicationCore.Enum;

namespace ApplicationCore.Specification.Filters
{
    public class AlumnoFilter : BaseFilter
    {
        public Genero Genero { get; set; }
        public string Apellido { get; set; }
        public string Codigo { get; set; }
    }
}
